using PassStore.Core.Interfaces;
using PassStore.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PassStore.WinForms.Forms;

public partial class LoginForm : Form
{
    private readonly IUserService _userService;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _configPath;
    public User? CurrentUser { get; private set; }

    public LoginForm(IUserService userService, IServiceProvider serviceProvider)
    {
        _userService = userService;
        _serviceProvider = serviceProvider;
        _configPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "userconfig.json");
        InitializeComponent();
        LoadSavedUser();
        SetupEnterKeyEvents();
    }

    private void SetupEnterKeyEvents()
    {
        txtKullaniciAdi.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { txtSifre.Focus(); } };
        txtSifre.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { btnLogin_Click(btnLogin, EventArgs.Empty); } };
        txtEmail.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { btnLogin_Click(btnLogin, EventArgs.Empty); } };
    }

    private void LoadSavedUser()
    {
        try
        {
            if (File.Exists(_configPath))
            {
                var json = File.ReadAllText(_configPath);
                var config = System.Text.Json.JsonSerializer.Deserialize<UserConfig>(json);
                if (config != null && config.RememberMe && !string.IsNullOrEmpty(config.KullaniciAdi))
                {
                    txtKullaniciAdi.Text = config.KullaniciAdi;
                    chkBeniHatirla.Checked = true;
                }
            }
        }
        catch
        {
            // Hata durumunda sessizce devam et
        }
    }

    private void SaveUserConfig(string kullaniciAdi, bool rememberMe)
    {
        try
        {
            var config = new UserConfig
            {
                KullaniciAdi = rememberMe ? kullaniciAdi : null,
                RememberMe = rememberMe
            };
            var json = System.Text.Json.JsonSerializer.Serialize(config);
            File.WriteAllText(_configPath, json);
        }
        catch
        {
            // Hata durumunda sessizce devam et
        }
    }

    public class UserConfig
    {
        public string? KullaniciAdi { get; set; }
        public bool RememberMe { get; set; }
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = await _userService.LoginAsync(txtKullaniciAdi.Text, txtSifre.Text);
            if (user != null)
            {
                CurrentUser = user;
                SaveUserConfig(txtKullaniciAdi.Text, chkBeniHatirla.Checked);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Giriş yapılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = await _userService.RegisterAsync(txtKullaniciAdi.Text, txtSifre.Text, txtEmail.Text ?? "");
            MessageBox.Show("Kayıt başarılı! Giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtSifre.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Kayıt yapılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
