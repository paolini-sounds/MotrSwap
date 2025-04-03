# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY MotrSwap/*.csproj ./MotrSwap/
RUN dotnet restore ./MotrSwap/MotrSwap.csproj

COPY . .
RUN dotnet publish ./MotrSwap/MotrSwap.csproj -c Release -o /out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "MotrSwap.dll"]
