# SonarQube Integration with Azure DevOps Pipelines

This guide walks through **end-to-end integration of SonarQube with Azure DevOps YAML pipelines**, from installation to enforcing Quality Gates.

---

## 1. Prerequisites

- Azure DevOps organization & project
- Azure DevOps pipeline (YAML-based)
- SonarQube **Community / Developer / Enterprise** edition
- Java (SonarQube requires Java)
- Permissions to create service connections

> ⚠️ **Important**
> - **Community Edition** ❌ does **NOT** support PR analysis or Quality Gate as branch policy
> - **Developer Edition+** ✅ supports PR analysis and branch policies

---

## 2. Install SonarQube

### Option A: Local / VM Installation (Linux example)

```bash
sudo apt update
sudo apt install openjdk-17-jdk unzip -y
```

Download SonarQube:

```bash
wget https://binaries.sonarsource.com/Distribution/sonarqube/sonarqube-10.x.zip
unzip sonarqube-10.x.zip
cd sonarqube-10.x/bin/linux-x86-64
./sonar.sh start
```

Access SonarQube:
```
http://<server-ip>:9000
```

Default credentials:
```
admin / admin
```

---

### Option B: Docker (Recommended for Dev/Test)

```bash
docker run -d \
  --name sonarqube \
  -p 9000:9000 \
  sonarqube:lts
```

---

## 3. Create SonarQube Project

1. Login to SonarQube UI
2. Click **Create Project**
3. Choose **Manually**
4. Enter:
   - Project Key
   - Project Name
5. Generate a **token** (save it securely)

---

## 4. Install SonarQube Extension in Azure DevOps

1. Go to **Azure DevOps → Organization Settings**
2. Open **Extensions**
3. Search **SonarQube** (by SonarSource)
4. Install it

---

## 5. Create SonarQube Service Connection

1. Azure DevOps → Project Settings
2. Service connections → New
3. Select **SonarQube**
4. Enter:
   - SonarQube Server URL
   - Authentication Token
5. Save & Verify

---

## 6. YAML Pipeline Integration

### 6.1 Install Required SDKs

```yaml
- task: UseDotNet@2
  inputs:
    packageType: sdk
    version: '8.x'
```

---

### 6.2 Prepare SonarQube Analysis

```yaml
- task: SonarQubePrepare@5
  inputs:
    SonarQube: 'SonarQube-Service-Connection'
    scannerMode: 'MSBuild'
    projectKey: 'pipeline-001'
    projectName: 'pipeline-001'
```

---

### 6.3 Build the Project

```yaml
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '**/*.csproj'
```

---

### 6.4 Run Tests with Code Coverage

```yaml
- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
    arguments: '--collect:"XPlat Code Coverage"'
```

---

### 6.5 Run SonarQube Analysis

```yaml
- task: SonarQubeAnalyze@5
```

---

### 6.6 Publish Quality Gate Result

```yaml
- task: SonarQubePublish@5
  inputs:
    pollingTimeoutSec: '300'
```

---

## 7. Full Sample Pipeline

```yaml
trigger:
- main

pool:
  vmImage: 'windows-latest'

steps:
- task: UseDotNet@2
  inputs:
    packageType: sdk
    version: '8.x'

- task: SonarQubePrepare@5
  inputs:
    SonarQube: 'SonarQube-Service-Connection'
    scannerMode: 'MSBuild'
    projectKey: 'pipeline-001'
    projectName: 'pipeline-001'

- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
    arguments: '--collect:"XPlat Code Coverage"'

- task: SonarQubeAnalyze@5

- task: SonarQubePublish@5
  inputs:
    pollingTimeoutSec: '300'
```

---

## 8. Enforce Quality Gate in Azure DevOps

### Option 1: Pipeline Failure
- `SonarQubePublish` fails pipeline if Quality Gate fails

### Option 2: Branch Policy (Developer Edition+ only)

1. Repos → Branches → main
2. Branch Policies
3. Add **Status Check**
4. Select **SonarQube Quality Gate**

---

## 9. Common Errors & Fixes

### ❌ PR analysis error in Community Edition
```
sonar.pullrequest.* requires Developer Edition
```
**Fix:**
- Remove PR parameters
- OR upgrade SonarQube edition

---

### ❌ No code coverage shown

**Fix:**
- Ensure test projects exist
- Use XPlat Code Coverage
- Add PublishCodeCoverageResults task if needed

---

## 10. Best Practices

- Always run SonarQube **before publish/deploy**
- Fail pipeline on Quality Gate failure
- Use Developer Edition for PR validation
- Separate Build, Test, Analyze stages

---

## 11. References

- https://docs.sonarsource.com/
- https://learn.microsoft.com/azure/devops/pipelines/

---

✅ You now have **end-to-end SonarQube integration with Azure DevOps pipelines**
