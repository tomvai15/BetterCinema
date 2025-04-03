# BetterCinema

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/8931716be1774cb5bdd33979f530e505)](https://www.codacy.com/gh/tomvai15/BetterCinema/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=tomvai15/BetterCinema&amp;utm_campaign=Badge_Grade)


```
dotnet tool install --global dotnet-ef
```

## HTTPS support

### Generate and trust certificate

Select any folder, for example `%USERPROFILE%\.aspnet\https`.
Generate certificate:
```ps
dotnet dev-certs https -ep ./certificate.pfx -p <PASSWORD> --trust
```
### Add certificate to .NET project
Specify environmental variables. Example for docker compose
```
services:
  webapp:
    ports:
      - "5001:443"                  
      - "5000:80"
    environment:
        # other
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_Kestrel__Certificates__Default__Password=<PASSWORD>
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/certificate.pfx
    volumes:
      - ~/.aspnet/https:/https:ro  # folder must match folder where certificate was created
```
