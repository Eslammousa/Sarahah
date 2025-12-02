# -------------------------
# STAGE 1: Build
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . .

# Restore UI project
RUN dotnet restore "sarahahSolution/sarahah.UI/sarahah.UI.csproj"

# Build the project
RUN dotnet publish "sarahahSolution/sarahah.UI/sarahah.UI.csproj" -c Release -o /out --no-restore


# -------------------------
# STAGE 2: Runtime
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "sarahah.UI.dll"]
