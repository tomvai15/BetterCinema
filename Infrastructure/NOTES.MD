# Setup azure

```
az group create -n cinemaRG -l westus

az ad sp create-for-rbac --name better-cinema --role contributor --scopes /subscriptions/b852789b-b85a-4c48-893d-01aedc58174c/resourceGroups/cinemaRG --json-auth
```


Gotchas
```
Error: Error: The subscription is not registered to use namespace 'Microsoft.ContainerInstance'. See https://aka.ms/rps-not-found for how to register subscriptions.
```

Navigate to `Subscriptions` and find `Resource providers` and add required namespace.
