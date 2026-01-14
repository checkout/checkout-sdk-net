# Secrets Management Documentation

## Overview

This document describes where and how secrets are stored and used in the checkout-sdk-net repository.

## GitHub Secrets Storage

The secret `IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` is stored in **GitHub Secrets**, which is a secure storage mechanism provided by GitHub for sensitive data used in GitHub Actions workflows.

### Where are GitHub Secrets stored?

GitHub Secrets are stored in the repository settings:
- Navigate to: **Repository → Settings → Secrets and variables → Actions**
- Secrets are encrypted and can only be accessed by GitHub Actions workflows
- They are never exposed in logs or visible to users

### Secret: IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID

**Location:** GitHub Repository Secrets (Settings → Secrets and variables → Actions)

**Purpose:** This secret contains the OAuth Client ID for the Default account system, used during integration testing.

**Usage:** The secret is referenced in GitHub Actions workflows and mapped to the environment variable `CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` during test execution.

## How Secrets are Used in GitHub Actions

The secret `IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` is used in the following GitHub Actions workflows:

### 1. build-pull-request.yml
```yaml
env:
  CHECKOUT_DEFAULT_OAUTH_CLIENT_ID: ${{ secrets.IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID }}
```
- **File:** `.github/workflows/build-pull-request.yml`
- **Section:** `build-and-test` step environment variables
- **Triggered on:** Pull requests to master branch

### 2. build-master.yml
```yaml
env:
  CHECKOUT_DEFAULT_OAUTH_CLIENT_ID: ${{ secrets.IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID }}
```
- **File:** `.github/workflows/build-master.yml`
- **Section:** `build-and-test` step environment variables
- **Triggered on:** Push to master branch

### 3. build-release.yml
```yaml
env:
  CHECKOUT_DEFAULT_OAUTH_CLIENT_ID: ${{ secrets.IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID }}
```
- **File:** `.github/workflows/build-release.yml`
- **Section:** `test-solution` step environment variables
- **Triggered on:** Push to master branch when Directory.Build.props changes

## Related Secrets

The following related secrets are also stored in GitHub Secrets:

| GitHub Secret Name | Environment Variable | Purpose |
|-------------------|---------------------|---------|
| `IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` | `CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` | OAuth Client ID for Default account |
| `IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET` | `CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET` | OAuth Client Secret for Default account |
| `IT_CHECKOUT_DEFAULT_OAUTH_PAYOUT_SCHEDULE_CLIENT_ID` | `CHECKOUT_DEFAULT_OAUTH_PAYOUT_SCHEDULE_CLIENT_ID` | OAuth Client ID for Payout Schedule |
| `IT_CHECKOUT_DEFAULT_OAUTH_PAYOUT_SCHEDULE_CLIENT_SECRET` | `CHECKOUT_DEFAULT_OAUTH_PAYOUT_SCHEDULE_CLIENT_SECRET` | OAuth Client Secret for Payout Schedule |
| `IT_CHECKOUT_DEFAULT_OAUTH_ACCOUNTS_CLIENT_ID` | `CHECKOUT_DEFAULT_OAUTH_ACCOUNTS_CLIENT_ID` | OAuth Client ID for Accounts |
| `IT_CHECKOUT_DEFAULT_OAUTH_ACCOUNTS_CLIENT_SECRET` | `CHECKOUT_DEFAULT_OAUTH_ACCOUNTS_CLIENT_SECRET` | OAuth Client Secret for Accounts |
| `IT_CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_ID` | `CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_ID` | OAuth Client ID for Issuing |
| `IT_CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_SECRET` | `CHECKOUT_DEFAULT_OAUTH_ISSUING_CLIENT_SECRET` | OAuth Client Secret for Issuing |
| `IT_CHECKOUT_DEFAULT_SECRET_KEY` | `CHECKOUT_DEFAULT_SECRET_KEY` | Secret Key for Default account (NAS) |
| `IT_CHECKOUT_DEFAULT_PUBLIC_KEY` | `CHECKOUT_DEFAULT_PUBLIC_KEY` | Public Key for Default account (NAS) |
| `IT_CHECKOUT_PREVIOUS_SECRET_KEY` | `CHECKOUT_PREVIOUS_SECRET_KEY` | Secret Key for Previous account (ABC) |
| `IT_CHECKOUT_PREVIOUS_PUBLIC_KEY` | `CHECKOUT_PREVIOUS_PUBLIC_KEY` | Public Key for Previous account (ABC) |
| `IT_CHECKOUT_PROCESSING_CHANNEL_ID` | `CHECKOUT_PROCESSING_CHANNEL_ID` | Processing Channel ID |
| `IT_CHECKOUT_MERCHANT_SUBDOMAIN` | `CHECKOUT_MERCHANT_SUBDOMAIN` | Merchant Subdomain |

## Code References

The environment variable `CHECKOUT_DEFAULT_OAUTH_CLIENT_ID` is accessed in the test code using:

```csharp
System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_OAUTH_CLIENT_ID")
```

### Files that use this environment variable:
- `test/CheckoutSdkTest/SandboxTestFixture.cs`
- `test/CheckoutSdkTest/Accounts/AccountsIntegrationTest.cs`
- `test/CheckoutSdkTest/Extensions/CheckoutServiceCollectionTest.cs`
- `test/CheckoutSdkTest/OAuthIntegrationTest.cs`

## Local Development

For local development and testing, you need to set these environment variables in your system:

### Windows (PowerShell)
```powershell
$env:CHECKOUT_DEFAULT_OAUTH_CLIENT_ID="your-client-id"
$env:CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET="your-client-secret"
```

### macOS/Linux (Bash)
```bash
export CHECKOUT_DEFAULT_OAUTH_CLIENT_ID="your-client-id"
export CHECKOUT_DEFAULT_OAUTH_CLIENT_SECRET="your-client-secret"
```

## Security Best Practices

1. **Never commit secrets to source code** - Secrets should only be stored in GitHub Secrets or environment variables
2. **Use GitHub Secrets for CI/CD** - All secrets used in GitHub Actions should be stored in repository secrets
3. **Rotate secrets regularly** - Update secrets periodically to maintain security
4. **Limit access** - Only repository administrators can view and modify GitHub Secrets
5. **Use descriptive names** - Secret names use the `IT_CHECKOUT_*` prefix to indicate they are integration test credentials

## Managing Secrets

### Adding a New Secret

1. Navigate to repository Settings → Secrets and variables → Actions
2. Click "New repository secret"
3. Enter the secret name (e.g., `IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID`)
4. Enter the secret value
5. Click "Add secret"

### Updating a Secret

1. Navigate to repository Settings → Secrets and variables → Actions
2. Find the secret you want to update
3. Click "Update" next to the secret
4. Enter the new value
5. Click "Update secret"

### Using Secrets in Workflows

Reference secrets in workflow files using the `${{ secrets.SECRET_NAME }}` syntax:

```yaml
env:
  CHECKOUT_DEFAULT_OAUTH_CLIENT_ID: ${{ secrets.IT_CHECKOUT_DEFAULT_OAUTH_CLIENT_ID }}
```

## Additional Resources

- [GitHub Actions - Using secrets](https://docs.github.com/en/actions/security-guides/using-secrets-in-github-actions)
- [README.md](README.md) - See "Building and testing" section for environment variable requirements
