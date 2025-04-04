# Setup azure


Create resource group
```
az group create -n <RESOURCE_GROUP_NAME> -l westus
```

Create service principal with owner role.
```
az ad sp create-for-rbac --name <PRINCIPAL_NAME> --role owner --scopes /subscriptions/<SUBSCRIPTION_ID>/resourceGroups/<RESOURCE_GROUP_NAME> --json-auth
```

Gotchas
```
Error: Error: The subscription is not registered to use namespace 'Microsoft.ContainerInstance'. See https://aka.ms/rps-not-found for how to register subscriptions.
```

Navigate to `Subscriptions` and find `Resource providers` and add required namespace.

## TODO
federated credentials