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
az cosmosdb sql role assignment create --resource-group "<name-of-existing-resource-group>" --account-name "<name-of-existing-nosql-account>" --role-definition-id "<id-of-new-role-definition>" --principal-id "<id-of-existing-identity>" --scope "/subscriptions/aaaa0a0a-bb1b-cc2c-dd3d-eeeeee4e4e4e/resourceGroups/msdocs-identity-example/providers/Microsoft.DocumentDB/databaseAccounts/msdocs-identity-example-nosql"<br>Link: https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/how-to-grant-data-plane-access?tabs=built-in-definition%2Ccsharp&pivots=azure-interface-cli#permission-model </li>
<li>Ran the functions & Updated the main.js with local API</li>


<br>
<h3>Step3: Deploy function to Azure & then to Static Web Apps</h3>
<h4>Deploy function to Azure</h4>
<li> Created function on az portal-using Windows (rg of function should be same as rg of db)</li>
<li> Commented out RID in api.csproject</li>
<li> In vscode, go to Azure->Workspace->click on the local project and deploy to Function</li>
<li> Updated cosmos db credentials(step2) On az portal In the func app env variables-application settings</li>
<li> Updated the main.js with production URL(in the func app->get function URL);enabled CORS</li>
<h4>Deployed to Static Web Apps </h4>
<li>Using static web apps because deploying using storage account is not free.</li>
<li>Created a static web app on the portal-choose token<br>
<li>To deploy to static web app without authenticating to git can only be possible using command:<br>
        => npm install -g @azure/static-web-apps-cli<br>
        => swa deploy ./ --app-name RayAzureStaicWebapp --env production --deployment-token YOUR_TOKEN_HERE<br>(./ is the folder where index.html is present)</li>
<li> Updated the URL of static WebApp in CORS </li>

<h4>Create a custom domain</h4>
<li>On the static web app->Custom Domains->Custom Domain on other DNS</LI>
<li> Added and validated the custom domain-rayeesasite.com</li>
<li>Updated the URL https://rayeesasite.com in CORS</li>





