# استفاده از تصویر پایه‌ی SDK برای ساخت پروژه
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RedisApi/RedisApi.csproj", "RedisApi/"]
RUN dotnet restore "RedisApi/RedisApi.csproj"
COPY . .
WORKDIR "/src/RedisApi"
RUN dotnet build "RedisApi.csproj" -c Release -o /app/build

# ساخت نسخه‌ی نهایی و انتشار پروژه
FROM build AS publish
RUN dotnet publish "RedisApi.csproj" -c Release -o /app/publish

# تعریف تصویر نهایی که از تصویر پایه‌ی ASP.NET Runtime استفاده می‌کند
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisApi.dll"]