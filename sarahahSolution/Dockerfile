# -------------------------
# STAGE 1: Build (.NET 9)
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore "sarahahSolution/sarahah.UI/sarahah.UI.csproj"
RUN dotnet publish "sarahahSolution/sarahah.UI/sarahah.UI.csproj" -c Release -o /out --no-restore

# -------------------------
# STAGE 2: Runtime (.NET 9)
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "sarahah.UI.dll"]
