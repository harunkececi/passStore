using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PassStore.Core.Interfaces;
using PassStore.Infrastructure.Data;
using PassStore.Infrastructure.Migrations;
using PassStore.WinForms.Forms;
using System.Reflection;
using System;
using System.Windows.Forms;
using System.IO;

namespace PassStore.WinForms;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var serviceProvider = DependencyInjection.ConfigureServices();
        
        // Veritabanı ve migration'ları başlat
        try
        {
            using (var scope = serviceProvider.CreateScope())
            {
                // FluentMigrator ile migration'ları çalıştır
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                
                // Migration'ları çalıştır
                System.Diagnostics.Debug.WriteLine("[PassStore] Migration'lar başlatılıyor...");
                runner.MigrateUp();
                System.Diagnostics.Debug.WriteLine("[PassStore] Migration'lar tamamlandı.");
                
            }
        }
        catch (Exception ex)
        {
            // Path bilgisini al (DependencyInjection ile aynı mantık)
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbDirectory = Path.Combine(localAppData, "PassStore");
            var dbPath = Path.Combine(dbDirectory, "passstore.db");
            
            System.Diagnostics.Debug.WriteLine($"[PassStore] Migration hatası: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[PassStore] Inner Exception: {ex.InnerException?.Message}");
            
            // Eğer tablo zaten varsa hatası ise, devam et (muhtemelen migration zaten çalıştırılmış)
            if (ex.Message.Contains("already exists") || ex.InnerException?.Message.Contains("already exists") == true)
            {
                System.Diagnostics.Debug.WriteLine("[PassStore] Tablo zaten var, devam ediliyor...");
                // "already exists" hatası ise sessizce devam et
            }
            else
            {
                var errorMessage = $"Veritabanı hatası: {ex.Message}\n\n" +
                    $"Veritabanı yolu: {dbPath}\n" +
                    $"Dizin mevcut: {(Directory.Exists(dbDirectory) ? "Evet" : "Hayır")}\n" +
                    $"Dosya mevcut: {(File.Exists(dbPath) ? "Evet" : "Hayır")}\n\n" +
                    $"Lütfen passstore.db dosyasını silip tekrar deneyin.\n" +
                    $"(Dosya konumu: {dbPath})";
                
                MessageBox.Show(errorMessage, 
                    "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Login formunu göster veya kayıtlı kullanıcı için direkt master password
        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var configPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "userconfig.json");
            PassStore.Core.Entities.User? currentUser = null;
            string? savedKullaniciAdi = null;
            string? masterPassword = null;

            // Kayıtlı kullanıcı kontrolü
            if (File.Exists(configPath))
            {
                try
                {
                    var json = File.ReadAllText(configPath);
                    var config = System.Text.Json.JsonSerializer.Deserialize<LoginForm.UserConfig>(json);
                    if (config != null && config.RememberMe && !string.IsNullOrEmpty(config.KullaniciAdi))
                    {
                        savedKullaniciAdi = config.KullaniciAdi;
                    }
                }
                catch
                {
                    // Hata durumunda normal login ekranına geç
                }
            }

            if (savedKullaniciAdi != null)
            {
                // Kayıtlı kullanıcı varsa, direkt master password ekranına geç
                using var masterPasswordForm = new MasterPasswordForm();
                if (masterPasswordForm.ShowDialog() == DialogResult.OK)
                {
                    masterPassword = masterPasswordForm.MasterPassword;
                    // Kullanıcıyı bul
                    currentUser = userService.GetUserByUsernameAsync(savedKullaniciAdi).GetAwaiter().GetResult();
                    if (currentUser == null)
                    {
                        MessageBox.Show("Kayıtlı kullanıcı bulunamadı. Lütfen tekrar giriş yapın.", 
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Config dosyasını temizle
                        try { File.Delete(configPath); } catch { }
                        // Normal login ekranına geç
                        var loginForm = new LoginForm(userService, serviceProvider);
                        if (loginForm.ShowDialog() == DialogResult.OK && loginForm.CurrentUser != null)
                        {
                            currentUser = loginForm.CurrentUser;
                            masterPassword = null; // Normal login'de master password tekrar sorulacak
                        }
                    }
                }
            }
            else
            {
                // Normal login ekranı
                var loginForm = new LoginForm(userService, serviceProvider);
                if (loginForm.ShowDialog() == DialogResult.OK && loginForm.CurrentUser != null)
                {
                    currentUser = loginForm.CurrentUser;
                }
            }

            // Ana form döngüsü - çıkış yapılana kadar devam et
            while (currentUser != null)
            {
                var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var mainForm = new MainForm(passwordService, unitOfWork, currentUser, userService, serviceProvider, masterPassword);
                mainForm.ShowDialog();
                
                // Eğer çıkış yapıldıysa (DialogResult.Abort), login ekranına geri dön
                if (mainForm.DialogResult == DialogResult.Abort)
                {
                    // Login ekranını tekrar göster
                    var loginForm = new LoginForm(userService, serviceProvider);
                    if (loginForm.ShowDialog() == DialogResult.OK && loginForm.CurrentUser != null)
                    {
                        currentUser = loginForm.CurrentUser;
                        masterPassword = null; // Yeni girişte master password tekrar sorulacak
                    }
                    else
                    {
                        // Login iptal edildi, uygulamayı kapat
                        currentUser = null;
                    }
                }
                else
                {
                    // Normal çıkış (X butonu veya Application.Exit)
                    currentUser = null;
                }
            }
        }
    }
}