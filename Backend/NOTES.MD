# Setup

Creates initial migration, need to create migration in data project and also specify api project
```
dotnet ef --startup-project ../BetterCinema.Api/ migrations add InitialCreate
```

Build docker image with full information.
```
docker build --progress=plain . --no-cache         
```