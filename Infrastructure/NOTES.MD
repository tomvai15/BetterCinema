# Setup azure

```
az group create -n cinemaRG -l westus

az ad sp create-for-rbac --name better-cinema --role contributor --scopes /subscriptions/b852789b-b85a-4c48-893d-01aedc58174c/resourceGroups/cinemaRG --json-auth
```