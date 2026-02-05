FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY UserManagementApp/*.csproj ./UserManagementApp/
RUN dotnet restore

COPY UserManagementApp/. ./UserManagementApp/
WORKDIR /app/UserManagementApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/UserManagementApp/out .
ENTRYPOINT ["dotnet", "UserManagementApp.dll"]