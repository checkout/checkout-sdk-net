# Checks the packages produced by `dotnet pack` before they are uploaded for publishing.
# Fails unless the directory holds exactly the two expected packages of the given version, each
# with only the expected entries and a nuspec naming the right id and version, whose dependency
# groups are exactly the package's committed packages.lock.json: one group per target framework,
# each listing every direct dependency of that framework at its locked version and nothing else
# (a project reference is CheckoutSDK at this version).
#
# Usage: pwsh .github/scripts/verify-packages.ps1 -Version 6.9.0 -Directory <dir>
# When a target framework is added or removed in a csproj, update $Packages below. A
# PackageReference change needs only the relocked packages.lock.json.

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
        Id         = 'CheckoutSDK'
        Assembly   = 'CheckoutSdk.dll'
        LockFile   = 'src/CheckoutSdk/packages.lock.json'
        Frameworks = @('net10.0', 'net8.0', 'net6.0', 'net5.0', 'net45', 'netstandard2.0')
    },
    @{
        Id         = 'CheckoutSDK.Extensions.Microsoft'
        Assembly   = 'CheckoutSdk.Extensions.dll'
        LockFile   = 'src/CheckoutSdk.Extensions/packages.lock.json'
        Frameworks = @('net10.0', 'net8.0', 'net6.0', 'net5.0', 'netstandard2.0')
    }
)

# How each target framework is named in a nuspec dependency group and in packages.lock.json.
$FrameworkNames = @{
    'net10.0'        = @{ Nuspec = 'net10.0'; Lock = 'net10.0' }
    'net8.0'         = @{ Nuspec = 'net8.0'; Lock = 'net8.0' }
    'net6.0'         = @{ Nuspec = 'net6.0'; Lock = 'net6.0' }
    'net5.0'         = @{ Nuspec = 'net5.0'; Lock = '.NETCoreApp,Version=v5.0' }
    'net45'          = @{ Nuspec = '.NETFramework4.5'; Lock = '.NETFramework,Version=v4.5' }
    'netstandard2.0' = @{ Nuspec = '.NETStandard2.0'; Lock = '.NETStandard,Version=v2.0' }
}

# Direct references that are never packed: the implicit netstandard2.0 library and the
# reference assemblies used only to compile net45 (PrivateAssets=All).
$BuildOnly = @('NETStandard.Library', 'Microsoft.NETFramework.ReferenceAssemblies')

$RepositoryRoot = Join-Path $PSScriptRoot '../..'

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

    if (@($nuspec.SelectNodes('//n:frameworkAssemblies | //n:frameworkReferences', $ns)).Count -gt 0) {
        Fail "$name declares framework references"
    }

    # Dependencies: exactly the direct dependencies of the committed lock file, per framework.
    # NuGet ids are case-insensitive: they are compared lower-cased; versions exactly.
    $lock = Get-Content -Raw -LiteralPath (Join-Path $RepositoryRoot $package.LockFile) | ConvertFrom-Json
    $groups = @($nuspec.SelectNodes('/n:package/n:metadata/n:dependencies/n:group', $ns))
    $dependencyNodes = @($nuspec.SelectNodes('//n:dependency', $ns))
    $grouped = @($nuspec.SelectNodes('/n:package/n:metadata/n:dependencies/n:group/n:dependency', $ns))
    if ($dependencyNodes.Count -ne $grouped.Count) { Fail "$name has a dependency outside a framework group" }
    $groupNames = @($groups | ForEach-Object { $_.GetAttribute('targetFramework') })
    $expectedGroups = @($package.Frameworks | ForEach-Object { $FrameworkNames[$_].Nuspec })
    if ((@($groupNames | Sort-Object) -join '|') -cne (@($expectedGroups | Sort-Object) -join '|')) {
        Fail "$name dependency groups are [$($groupNames -join ', ')], expected [$($expectedGroups -join ', ')]"
    }
    foreach ($framework in $package.Frameworks) {
        $names = $FrameworkNames[$framework]
        $locked = $lock.dependencies.($names.Lock)
        if ($null -eq $locked) { Fail "$($package.LockFile) has no $($names.Lock) section" }
        $expected = @(foreach ($entry in $locked.PSObject.Properties) {
                $type = $entry.Value.type
                if ($type -ceq 'Project') {
                    "checkoutsdk $Version"
                }
                elseif ($type -ceq 'Direct' -and -not ($BuildOnly | Where-Object { $_ -ieq $entry.Name })) {
                    $requested = [string] $entry.Value.requested
                    if ($requested -cmatch '\A\[([^,\]]+), \)\z') { $requested = $Matches[1] }
                    "$($entry.Name.ToLowerInvariant()) $requested"
                }
            })
        $group = @($groups | Where-Object { $_.GetAttribute('targetFramework') -ceq $names.Nuspec })[0]
        $actual = @(foreach ($dependency in @($group.SelectNodes('n:dependency', $ns))) {
                "$($dependency.GetAttribute('id').ToLowerInvariant()) $($dependency.GetAttribute('version'))"
            })
        $expectedText = (@($expected | Sort-Object) -join ', ')
        $actualText = (@($actual | Sort-Object) -join ', ')
        if ($actualText -cne $expectedText) {
            Fail "$name $framework dependencies are [$actualText], expected [$expectedText] from $($package.LockFile)"
        }
    }
    if ($package.Id -ceq 'CheckoutSDK' -and ($grouped | Where-Object { $_.GetAttribute('id') -ieq 'CheckoutSDK' })) {
        Fail "$name depends on itself"
    }
    Write-Host "verify-packages: $name OK"
}
