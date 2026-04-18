1. In the vs code install **bicep**  
2. Install bicep for the cli by command:   

```bash
az bicep install
az bicep version
```  
**How to run the bicep to execute/create the resoures**  
1. Create main.bicep in a folder and open it into vs code to create the **storage account** for example
2. type storage "In the intelligence" you would get **res-storage** select that. you would get code created with that as below:  

```bash
param storageName string = 'stg${uniqueString(resourceGroup().id)}'
param location string= resourceGroup().location

resource storageaccount 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: storageName
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Premium_LRS'
  }
}

```
**To generage the ARM template from this .bicep file**  
```bash
az bicep build -f .\main.bicep
```
To create resource group by azure cli command is:  

```bash
az group create --name Bicep --locaton eastus
```
**To execute the .bicep file to create storage accout is:  **
```bash
az deployment group create --resouce-group Bicep --template-file main.bicep --parameters storageName=yorstoragename
```
