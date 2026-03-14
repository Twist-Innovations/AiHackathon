# Use the official .NET 10.0 ASP.NET runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET 10.0 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["AiHackathon.ApiService/AiHackathon.ApiService.csproj", "AiHackathon.ApiService/"]
COPY ["AiHackathon.ServiceDefaults/AiHackathon.ServiceDefaults.csproj", "AiHackathon.ServiceDefaults/"]
RUN dotnet restore "AiHackathon.ApiService/AiHackathon.ApiService.csproj"

# Copy the entire source code
COPY . .

# Build the application
WORKDIR "/src/AiHackathon.ApiService"
RUN dotnet build "AiHackathon.ApiService.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "AiHackathon.ApiService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Create the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AiHackathon.ApiService.dll"]