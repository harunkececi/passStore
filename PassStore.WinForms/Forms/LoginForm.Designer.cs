namespace PassStore.WinForms.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtKullaniciAdi;
    private TextBox txtSifre;
    private TextBox txtEmail;
    private Button btnLogin;
    private Button btnRegister;
    private Label lblKullaniciAdi;
    private Label lblSifre;
    private Label lblEmail;
    private CheckBox chkBeniHatirla;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.txtKullaniciAdi = new TextBox();
        this.txtSifre = new TextBox();
        this.txtEmail = new TextBox();
        this.btnLogin = new Button();
        this.btnRegister = new Button();
        this.lblKullaniciAdi = new Label();
        this.lblSifre = new Label();
        this.lblEmail = new Label();
        this.chkBeniHatirla = new CheckBox();
        this.SuspendLayout();

        // Form ayarları
        this.BackColor = Color.FromArgb(245, 247, 250);
        this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblKullaniciAdi
        this.lblKullaniciAdi.AutoSize = true;
        this.lblKullaniciAdi.Location = new Point(40, 50);
        this.lblKullaniciAdi.Name = "lblKullaniciAdi";
        this.lblKullaniciAdi.Size = new Size(100, 19);
        this.lblKullaniciAdi.Text = "Kullanıcı Adı:";
        this.lblKullaniciAdi.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblKullaniciAdi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtKullaniciAdi
        this.txtKullaniciAdi.Location = new Point(40, 75);
        this.txtKullaniciAdi.Name = "txtKullaniciAdi";
        this.txtKullaniciAdi.Size = new Size(320, 25);
        this.txtKullaniciAdi.TabIndex = 0;
        this.txtKullaniciAdi.BorderStyle = BorderStyle.FixedSingle;
        this.txtKullaniciAdi.BackColor = Color.White;
        this.txtKullaniciAdi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtKullaniciAdi.Padding = new Padding(5);

        // lblSifre
        this.lblSifre.AutoSize = true;
        this.lblSifre.Location = new Point(40, 115);
        this.lblSifre.Name = "lblSifre";
        this.lblSifre.Size = new Size(45, 19);
        this.lblSifre.Text = "Şifre:";
        this.lblSifre.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtSifre
        this.txtSifre.Location = new Point(40, 140);
        this.txtSifre.Name = "txtSifre";
        this.txtSifre.PasswordChar = '●';
        this.txtSifre.Size = new Size(320, 25);
        this.txtSifre.TabIndex = 1;
        this.txtSifre.BorderStyle = BorderStyle.FixedSingle;
        this.txtSifre.BackColor = Color.White;
        this.txtSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblEmail
        this.lblEmail.AutoSize = true;
        this.lblEmail.Location = new Point(40, 180);
        this.lblEmail.Name = "lblEmail";
        this.lblEmail.Size = new Size(50, 19);
        this.lblEmail.Text = "Email:";
        this.lblEmail.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtEmail
        this.txtEmail.Location = new Point(40, 205);
        this.txtEmail.Name = "txtEmail";
        this.txtEmail.Size = new Size(320, 25);
        this.txtEmail.TabIndex = 2;
        this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
        this.txtEmail.BackColor = Color.White;
        this.txtEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // chkBeniHatirla
        this.chkBeniHatirla.AutoSize = true;
        this.chkBeniHatirla.Location = new Point(40, 245);
        this.chkBeniHatirla.Name = "chkBeniHatirla";
        this.chkBeniHatirla.Size = new Size(105, 23);
        this.chkBeniHatirla.TabIndex = 5;
        this.chkBeniHatirla.Text = "Beni Hatırla";
        this.chkBeniHatirla.ForeColor = Color.FromArgb(51, 51, 51);
        this.chkBeniHatirla.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // btnLogin
        this.btnLogin.Location = new Point(40, 285);
        this.btnLogin.Name = "btnLogin";
        this.btnLogin.Size = new Size(155, 40);
        this.btnLogin.TabIndex = 3;
        this.btnLogin.Text = "Giriş Yap";
        this.btnLogin.BackColor = Color.FromArgb(74, 144, 226);
        this.btnLogin.ForeColor = Color.White;
        this.btnLogin.FlatStyle = FlatStyle.Flat;
        this.btnLogin.FlatAppearance.BorderSize = 0;
        this.btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnLogin.Cursor = Cursors.Hand;
        this.btnLogin.Click += new EventHandler(btnLogin_Click);
        this.btnLogin.MouseEnter += (s, e) => { this.btnLogin.BackColor = Color.FromArgb(65, 130, 200); };
        this.btnLogin.MouseLeave += (s, e) => { this.btnLogin.BackColor = Color.FromArgb(74, 144, 226); };

        // btnRegister
        this.btnRegister.Location = new Point(205, 285);
        this.btnRegister.Name = "btnRegister";
        this.btnRegister.Size = new Size(155, 40);
        this.btnRegister.TabIndex = 4;
        this.btnRegister.Text = "Kayıt Ol";
        this.btnRegister.BackColor = Color.FromArgb(108, 117, 125);
        this.btnRegister.ForeColor = Color.White;
        this.btnRegister.FlatStyle = FlatStyle.Flat;
        this.btnRegister.FlatAppearance.BorderSize = 0;
        this.btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnRegister.Cursor = Cursors.Hand;
        this.btnRegister.Click += new EventHandler(btnRegister_Click);
        this.btnRegister.MouseEnter += (s, e) => { this.btnRegister.BackColor = Color.FromArgb(90, 98, 104); };
        this.btnRegister.MouseLeave += (s, e) => { this.btnRegister.BackColor = Color.FromArgb(108, 117, 125); };

        // LoginForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(400, 360);
        this.Controls.Add(this.chkBeniHatirla);
        this.Controls.Add(this.btnRegister);
        this.Controls.Add(this.btnLogin);
        this.Controls.Add(this.txtEmail);
        this.Controls.Add(this.lblEmail);
        this.Controls.Add(this.txtSifre);
        this.Controls.Add(this.lblSifre);
        this.Controls.Add(this.txtKullaniciAdi);
        this.Controls.Add(this.lblKullaniciAdi);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LoginForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "🔐 Şifre Saklama - Giriş";
        this.Padding = new Padding(0);
        // Icon ekle
        try
        {
            var iconPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "app.ico");
            if (System.IO.File.Exists(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }
        }
        catch
        {
            // Icon yüklenemezse sessizce devam et
        }
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
