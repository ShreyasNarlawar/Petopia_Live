# 1. Use the .NET 8 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy the project file and install dependencies
COPY ["PetopiaWebApi.csproj", "./"]
RUN dotnet restore "PetopiaWebApi.csproj"

# 3. Copy the rest of the code and build the release
COPY . .
RUN dotnet publish "PetopiaWebApi.csproj" -c Release -o /app/publish

# 4. Use the lighter .NET 8 runtime for the live server
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 5. Tell Render to run it on port 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "PetopiaWebApi.dll"]