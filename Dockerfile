# Stage 1: Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy project files
COPY LuccaExpenses.Api/LuccaExpenses.Api.csproj LuccaExpenses.Api/
COPY LuccaExpenses.Tests/LuccaExpenses.Tests.csproj LuccaExpenses.Tests/

# Restore the projects
RUN dotnet restore "./LuccaExpenses.Api/LuccaExpenses.Api.csproj"
RUN dotnet restore "./LuccaExpenses.Tests/LuccaExpenses.Tests.csproj"

# Copy the rest of the files
COPY . .

# Build the application and the tests
RUN dotnet build "./LuccaExpenses.Api/LuccaExpenses.Api.csproj" -c Release -o /app/build
RUN dotnet build "./LuccaExpenses.Tests/LuccaExpenses.Tests.csproj" -c Release -o /app/tests

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "./LuccaExpenses.Api/LuccaExpenses.Api.csproj" -c Release -o /app/publish

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published files from the publish stage
COPY --from=publish /app/publish .

# Copy the test results (if necessary)
COPY --from=build /app/tests /app/tests

# Set the entry point for the container
ENTRYPOINT ["dotnet", "LuccaExpenses.Api.dll"]