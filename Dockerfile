# Set the working directory
USER app
WORKDIR /app
EXPOSE 8080

# Stage 1: Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["Lucca.ExpenseApp.Api/Lucca.ExpenseApp.Api.csproj", "Lucca.ExpenseApp.Api/"]
COPY ["Lucca.ExpenseApp.Application/Lucca.ExpenseApp.Application.csproj", "Lucca.ExpenseApp.Application/"]
COPY ["Lucca.ExpenseApp.Domain/Lucca.ExpenseApp.Domain.csproj", "Lucca.ExpenseApp.Domain/"]
COPY ["Lucca.ExpenseApp.Dto/Lucca.ExpenseApp.Dto.csproj", "Lucca.ExpenseApp.Dto/"]
COPY ["Lucca.ExpenseApp.Infrastructure/Lucca.ExpenseApp.Infrastructure.csproj", "Lucca.ExpenseApp.Infrastructure/"]
COPY ["Lucca.ExpenseApp.Tests/Lucca.ExpenseApp.Tests.csproj", "Lucca.ExpenseApp.Tests/"]"

# Restore the projects
RUN dotnet restore "./Lucca.ExpenseApp.Api/Lucca.ExpenseApp.Api.csproj"
RUN dotnet restore "./Lucca.ExpenseApp.Tests/Lucca.ExpenseApp.Tests.csproj"

# Copy the rest of the files
COPY . .

# Build the application and the tests
RUN dotnet build "./Lucca.ExpenseApp.Api/Lucca.ExpenseApp.Api.csproj" -c Release -o /app/build
RUN dotnet build "./Lucca.ExpenseApp.Tests/Lucca.ExpenseApp.Tests.csproj" -c Release -o /app/tests

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "./Lucca.ExpenseApp.Api/Lucca.ExpenseApp.Api.csproj" -c Release -o /app/publish

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published files from the publish stage
COPY --from=publish /app/publish .

# Copy the test results (if necessary)
COPY --from=build /app/tests /app/tests

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Lucca.ExpenseApp.Api.dll"]