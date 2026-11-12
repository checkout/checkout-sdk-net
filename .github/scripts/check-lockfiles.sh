#!/usr/bin/env bash
# Run "before", then "restore", then "after"; run "after" again after build, pack or test.
#   before:  every tracked project has a tracked packages.lock.json next to it, and no project
#            file changes how restore uses lock files or downloads packages outside them. NuGet's
#            locked mode only applies when a lock file exists; without one it resolves and writes
#            a new lock file, so a missing lock file must be refused here.
#   restore: locked restore with only the root nuget.config, into NUGET_PACKAGES, with no
#            fallback folder. Command-line properties override anything a project file sets, so
#            every package restore uses lands in the folder that "after" checks.
#   after:   restore changed no lock file and created none (ignored paths included), and every
#            package in NUGET_PACKAGES is pinned by a committed lock file.
# The workflows also set MSBUILDDISABLENUGETSDKRESOLVER, so no MSBuild project SDK is downloaded.
# Lock file settings live only in Directory.Build.props.
set -euo pipefail

mode="${1:-}"
cd "$(git rev-parse --show-toplevel)"

case "$mode" in
  before)
    status=0
    projects="$(git ls-files -- '*.csproj' '*.vbproj' '*.fsproj' '*.proj')"
    if [ -z "$projects" ]; then
      echo "check-lockfiles: no project found" >&2
      exit 1
    fi
    while IFS= read -r project; do
      lock="$(dirname "$project")/packages.lock.json"
      if ! git ls-files --error-unmatch -- "$lock" >/dev/null 2>&1; then
        echo "check-lockfiles: $project has no committed $lock (run .github/scripts/update-lockfiles.sh)" >&2
        status=1
      fi
    done <<< "$projects"
    # Restore inputs that would bypass or relocate the lock files.
    overrides="$(git grep -n -I -i -E \
      'NuGetLockFilePath|RestorePackagesWithLockFile|RestoreLockedMode|RestorePackagesPath|RestoreFallbackFolders|RestoreAdditionalProject|TreatAsLocalProperty|<PackageDownload|<Sdk[[:space:]]|Sdk="[^"]*/' \
      -- '*.*proj' '*.props' '*.targets' '*.user' ':!Directory.Build.props' || true)"
    if [ -n "$overrides" ]; then
      echo "check-lockfiles: lock file and restore settings belong in Directory.Build.props only:" >&2
      echo "$overrides" >&2
      status=1
    fi
    if git grep -q -i 'msbuild-sdks' -- global.json 2>/dev/null; then
      echo "check-lockfiles: global.json must not declare msbuild-sdks" >&2
      status=1
    fi
    rsp="$(git ls-files -- '*.rsp')"
    if [ -n "$rsp" ]; then
      echo "check-lockfiles: MSBuild response files are not allowed: $rsp" >&2
      status=1
    fi
    exit "$status"
    ;;
  restore)
    if [ -z "${NUGET_PACKAGES:-}" ]; then
      echo "check-lockfiles: NUGET_PACKAGES must point at this job's package folder" >&2
      exit 1
    fi
    exec dotnet restore --locked-mode --configfile nuget.config --packages "$NUGET_PACKAGES" \
      -p:RestoreFallbackFolders= -p:RestoreAdditionalProjectFallbackFolders= \
      -p:RestoreAdditionalProjectSources=
    ;;
  after)
    if ! git diff --quiet -- '*packages.lock.json'; then
      echo "check-lockfiles: restore changed a committed lock file (run .github/scripts/update-lockfiles.sh)" >&2
      git diff --stat -- '*packages.lock.json' >&2
      exit 1
    fi
    untracked="$(git ls-files --others -- '*packages.lock.json')"
    if [ -n "$untracked" ]; then
      echo "check-lockfiles: restore created lock files that are not committed:" >&2
      echo "$untracked" >&2
      exit 1
    fi
    # Every package NuGet put in this job's package folder, however a project asked for it
    # (PackageReference, PackageDownload, MSBuild project SDK, another config), must be pinned by
    # a committed lock file. Only the reference packs the .NET SDK downloads for older target
    # frameworks are exempt: lock files cannot record them and the SDK fixes their versions.
    packages="${NUGET_PACKAGES:-}"
    if [ -z "$packages" ]; then
      echo "check-lockfiles: NUGET_PACKAGES must point at this job's package folder" >&2
      exit 1
    fi
    if command -v cygpath > /dev/null 2>&1; then
      packages="$(cygpath -u "$packages")"
    fi
    if [ ! -d "$packages" ]; then
      echo "check-lockfiles: no package folder at NUGET_PACKAGES" >&2
      exit 1
    fi
    locked="$(git ls-files -z -- '*packages.lock.json' \
      | xargs -0 jq -r '.dependencies[] | to_entries[] | select(.value.type != "Project")
          | "\(.key | ascii_downcase)/\(.value.resolved | ascii_downcase)"' \
      | tr -d '\r' | LC_ALL=C sort -u)"
    # Only <id>/<version> directories are expected: refuse links, loose files and empty ids.
    odd="$(cd "$packages" && { find . -mindepth 1 -maxdepth 2 \( -type l -o ! -type d \) -print
      find . -mindepth 1 -maxdepth 1 -type d -empty -print; })"
    if [ -n "$odd" ]; then
      echo "check-lockfiles: unexpected entries in the package folder:" >&2
      echo "$odd" >&2
      exit 1
    fi
    unexpected="$(cd "$packages" && find . -mindepth 2 -maxdepth 2 -type d | sed 's#^\./##' | LC_ALL=C sort \
      | LC_ALL=C comm -23 - <(printf '%s\n' "$locked") \
      | grep -v -E '^(microsoft\.netcore\.app\.ref|microsoft\.aspnetcore\.app\.ref)/' || true)"
    if [ -n "$unexpected" ]; then
      echo "check-lockfiles: packages restored that no committed lock file pins:" >&2
      echo "$unexpected" >&2
      exit 1
    fi
    ;;
  *)
    echo "usage: check-lockfiles.sh before|restore|after" >&2
    exit 2
    ;;
esac
