# Checks the packages produced by `dotnet pack` before they are uploaded for publishing.
# Fails unless the directory holds exactly the two expected packages of the given version, each
# with only the expected entries and a nuspec naming the right id, version and dependencies.
#
# Usage: pwsh .github/scripts/verify-packages.ps1 -Version 6.9.0 -Directory <dir>
# When a target framework or a PackageReference is added or removed in a csproj, update
# $Packages below.

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string] $Version,
    [Parameter(Mandatory = $true)][string] $Directory
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
Add-Type -AssemblyName System.IO.Compression
Add-Type -AssemblyName System.IO.Compression.FileSystem

$Packages = @(
    @{
        Id           = 'CheckoutSDK'
        Assembly     = 'CheckoutSdk.dll'
        Frameworks   = @('net10.0', 'net8.0', 'net6.0', 'net5.0', 'net45', 'netstandard2.0')
        Dependencies = @('Microsoft.CSharp', 'Microsoft.Extensions.Logging', 'Newtonsoft.Json', 'System.Net.Http')
    },
    @{
        Id           = 'CheckoutSDK.Extensions.Microsoft'
        Assembly     = 'CheckoutSdk.Extensions.dll'
        Frameworks   = @('net10.0', 'net8.0', 'net6.0', 'net5.0', 'netstandard2.0')
        Dependencies = @('CheckoutSDK', 'Microsoft.Extensions.Configuration.Abstractions', 'Microsoft.Extensions.Configuration.Binder', 'Microsoft.Extensions.DependencyInjection')
    }
)

function Fail([string] $Message) {
    Write-Error "verify-packages: $Message" -ErrorAction Continue
    exit 1
}

if ($Version -cnotmatch '\A(0|[1-9][0-9]*)\.(0|[1-9][0-9]*)\.(0|[1-9][0-9]*)(-[0-9A-Za-z]+(\.[0-9A-Za-z]+)*)?\z') {
    Fail 'version is not MAJOR.MINOR.PATCH[-PRERELEASE]'
}

$dir = Get-Item -LiteralPath $Directory
if (-not $dir.PSIsContainer -or $dir.LinkType) { Fail 'package directory is not a plain directory' }

$expectedNames = $Packages | ForEach-Object { "$($_.Id).$Version.nupkg" }
$actual = @(Get-ChildItem -LiteralPath $dir.FullName -Force)
$actualNames = @($actual | ForEach-Object { $_.Name })
$unexpected = @($actualNames | Where-Object { $expectedNames -cnotcontains $_ })
$missing = @($expectedNames | Where-Object { $actualNames -cnotcontains $_ })
if ($unexpected.Count -gt 0 -or $missing.Count -gt 0) {
    Fail "package directory must hold exactly: $($expectedNames -join ', '). Unexpected: [$($unexpected -join ', ')]; missing: [$($missing -join ', ')]"
}
foreach ($item in $actual) {
    if ($item.PSIsContainer -or $item.LinkType) { Fail "$($item.Name) is not a regular file" }
}

foreach ($package in $Packages) {
    $name = "$($package.Id).$Version.nupkg"
    $allowed = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::Ordinal)
    [void] $allowed.Add('_rels/.rels')
    [void] $allowed.Add('[Content_Types].xml')
    [void] $allowed.Add("$($package.Id).nuspec")
    foreach ($framework in $package.Frameworks) { [void] $allowed.Add("lib/$framework/$($package.Assembly)") }

    $zip = [System.IO.Compression.ZipFile]::OpenRead((Join-Path $dir.FullName $name))
    try {
        $seen = [System.Collections.Generic.HashSet[string]]::new([System.StringComparer]::OrdinalIgnoreCase)
        $nuspecXml = $null
        foreach ($entry in $zip.Entries) {
            $path = $entry.FullName
            if (-not $seen.Add($path)) { Fail "$name has a duplicate entry" }
            $isCoreProperties = $path -cmatch '\Apackage/services/metadata/core-properties/(nuget|[0-9a-f]{32})\.psmdcp\z'
            if (-not $allowed.Contains($path) -and -not $isCoreProperties) {
                Fail "$name has an unexpected entry: $path"
            }
            if ($path -ceq "$($package.Id).nuspec") {
                $reader = [System.IO.StreamReader]::new($entry.Open())
                try { $nuspecXml = $reader.ReadToEnd() } finally { $reader.Dispose() }
            }
        }
        foreach ($required in $allowed) {
            if (-not $seen.Contains($required)) { Fail "$name is missing $required" }
        }
    }
    finally {
        $zip.Dispose()
    }

    $settings = [System.Xml.XmlReaderSettings]::new()
    $settings.DtdProcessing = [System.Xml.DtdProcessing]::Prohibit
    $settings.XmlResolver = $null
    $xmlReader = [System.Xml.XmlReader]::Create([System.IO.StringReader]::new($nuspecXml), $settings)
    $nuspec = [System.Xml.XmlDocument]::new()
    try { $nuspec.Load($xmlReader) } finally { $xmlReader.Dispose() }
    $ns = [System.Xml.XmlNamespaceManager]::new($nuspec.NameTable)
    $ns.AddNamespace('n', $nuspec.DocumentElement.NamespaceURI)

    $id = $nuspec.SelectSingleNode('/n:package/n:metadata/n:id', $ns)
    $ver = $nuspec.SelectSingleNode('/n:package/n:metadata/n:version', $ns)
    if ($null -eq $id -or $id.InnerText -cne $package.Id) { Fail "$name nuspec id is not $($package.Id)" }
    if ($null -eq $ver -or $ver.InnerText -cne $Version) { Fail "$name nuspec version is not $Version" }

    $nuspecNs = $nuspec.DocumentElement.NamespaceURI
    foreach ($element in $nuspec.SelectNodes('//*')) {
        if ($element.NamespaceURI -cne $nuspecNs) { Fail "$name nuspec has an element outside the nuspec namespace" }
    }
    if ($null -ne $nuspec.SelectSingleNode('//n:packageTypes', $ns)) { Fail "$name declares package types" }

    # NuGet ids are case-insensitive: compare them that way.
    $dependencies = @($nuspec.SelectNodes('//n:dependency', $ns))
    foreach ($dependency in $dependencies) {
        $depId = $dependency.GetAttribute('id')
        if (-not ($package.Dependencies | Where-Object { $_ -ieq $depId })) {
            Fail "$name has an unexpected dependency: $depId"
        }
    }
    if (@($nuspec.SelectNodes('//n:frameworkAssemblies | //n:frameworkReferences', $ns)).Count -gt 0) {
        Fail "$name declares framework references"
    }
    $sdkDependencies = @($dependencies | Where-Object { $_.GetAttribute('id') -ieq 'CheckoutSDK' })
    if ($package.Id -ceq 'CheckoutSDK') {
        if ($sdkDependencies.Count -gt 0) { Fail "$name depends on itself" }
    }
    else {
        if ($sdkDependencies.Count -ne $package.Frameworks.Count) { Fail "$name must depend on CheckoutSDK in every framework group" }
        foreach ($dependency in $sdkDependencies) {
            if ($dependency.GetAttribute('version') -cne $Version) { Fail "$name depends on a CheckoutSDK version other than $Version" }
        }
    }
    Write-Host "verify-packages: $name OK"
}
