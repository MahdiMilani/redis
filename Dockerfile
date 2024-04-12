# استفاده از پایه‌ی تصویر .NET 8.0 SDK برای ساختن پروژه
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# کپی فایل‌های csproj و بازسازی وابستگی‌ها
COPY ["RedisAPI.csproj", "./"]
RUN dotnet restore "RedisAPI.csproj"

# کپی تمام فایل‌های منبع
COPY . .

# ساختن پروژه
RUN dotnet publish "RedisAPI.csproj" -c Release -o /app/publish

# استفاده از تصویر Runtime برای اجرای پروژه
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# تنظیم پورت شبکه
EXPOSE 80

# تنظیم دستور اجرای کانتینر
ENTRYPOINT ["dotnet", "RedisAPI.dll"]
