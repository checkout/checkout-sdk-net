#!/usr/bin/env bash
# Regenerates every packages.lock.json after a PackageReference change, with the local .NET SDK.
# Commit the changed lock files together with the project change; CI refuses a stale lock file.
# A version bump alone changes no lock file.
#
# Usage: .github/scripts/update-lockfiles.sh
set -euo pipefail

root="$(git rev-parse --show-toplevel)"
cd "$root"

# CI=true turns on locked mode (Directory.Build.props), which would refuse to rewrite the locks.
env -u CI DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_NOLOGO=1 \
  dotnet restore --force-evaluate --configfile nuget.config

git status --short -- '*packages.lock.json'
