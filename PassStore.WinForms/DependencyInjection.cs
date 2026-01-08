using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PassStore.Application.Services;
using PassStore.Core.Interfaces;
using PassStore.Infrastructure.Data;
using PassStore.Infrastructure.Repositories;
using PassStore.Infrastructure.Services;
using System.IO;
using System.Reflection;
using System;

namespace PassStore.WinForms;

public static class DependencyInjection
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Uygulama dizinini belirle (kurulum sonrası doğru çalışması için)
        var appDirectory = System.Windows.Forms.Application.StartupPath ?? Directory.GetCurrentDirectory();
        
        // Veritabanını kullanıcının yazabileceği bir yere koy (izin sorunlarını önlemek için)
        // AppData\Local\PassStore klasörünü kullan
        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbDirectory = Path.Combine(localAppData, "PassStore");
        
        // Veritabanı dizinini oluştur (yoksa)
        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }
        
        var dbPath = Path.Combine(dbDirectory, "passstore.db");
        
        // Debug için path bilgisini logla
        System.Diagnostics.Debug.WriteLine($"[PassStore] App Directory: {appDirectory}");
        System.Diagnostics.Debug.WriteLine($"[PassStore] Local AppData: {localAppData}");
        System.Diagnostics.Debug.WriteLine($"[PassStore] DB Directory: {dbDirectory}");
        System.Diagnostics.Debug.WriteLine($"[PassStore] DB Path: {dbPath}");
        System.Diagnostics.Debug.WriteLine($"[PassStore] DB Directory Exists: {Directory.Exists(dbDirectory)}");
        System.Diagnostics.Debug.WriteLine($"[PassStore] DB File Exists: {File.Exists(dbPath)}");
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(appDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        // Connection string'i mutlak yol ile oluştur
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString) || connectionString.Contains("passstore.db"))
        {
            // Mutlak yol kullan
            connectionString = $"Data Source={dbPath}";
        }
        else if (!Path.IsPathRooted(connectionString.Replace("Data Source=", "").Trim()))
        {
            // Göreceli yol ise mutlak yola çevir
            var relativePath = connectionString.Replace("Data Source=", "").Trim();
            connectionString = $"Data Source={Path.Combine(appDirectory, relativePath)}";
        }
        
        // Connection string'i logla
        System.Diagnostics.Debug.WriteLine($"[PassStore] Connection String: {connectionString}");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        // FluentMigrator
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetAssembly(typeof(PassStore.Infrastructure.Migrations.CreateUsersTable))).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<EncryptionService>();

        return services.BuildServiceProvider();
    }
}
