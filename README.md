# AzureResumeProject

<h3>Step 1: Updated frontend</h3>
<li> Updated index.html</li>
<li> Added Js code</li>

<br>
<h3>Step 2: Updated Functions Locally</h3>
<li> Created CosmosBD account</li>
<li> Created a sp to access the database </li>
<li> Updated local.settings.json with database credentials and credentials for sp(tenant id,client id & secret)</li>
<li> RBAC issues to solved using the command:
az cosmosdb sql role assignment create --resource-group "<name-of-existing-resource-group>" --account-name "<name-of-existing-nosql-account>" --role-definition-id "<id-of-new-role-definition>" --principal-id "<id-of-existing-identity>" --scope "/subscriptions/aaaa0a0a-bb1b-cc2c-dd3d-eeeeee4e4e4e/resourceGroups/msdocs-identity-example/providers/Microsoft.DocumentDB/databaseAccounts/msdocs-identity-example-nosql" </li>
<li>Ran the functions & Updated the main.js with local API</li>

