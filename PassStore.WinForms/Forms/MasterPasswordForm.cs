namespace PassStore.WinForms.Forms;

public partial class MasterPasswordForm : Form
{
    private TextBox txtMasterPassword;
    private Button btnOK;
    private Label lblMasterPassword;
    public string MasterPassword { get; private set; } = string.Empty;

    public MasterPasswordForm()
    {
        InitializeComponent();
        SetupEnterKeyEvent();
    }

    private void SetupEnterKeyEvent()
    {
        txtMasterPassword.KeyDown += (s, e) => 
        { 
            if (e.KeyCode == Keys.Enter) 
            { 
                BtnOK_Click(btnOK, EventArgs.Empty); 
            } 
        };
    }

    private void InitializeComponent()
    {
        this.txtMasterPassword = new TextBox();
        this.btnOK = new Button();
        this.lblMasterPassword = new Label();

        this.SuspendLayout();

        // Form ayarları
        this.BackColor = Color.FromArgb(245, 247, 250);
        this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblMasterPassword
        this.lblMasterPassword.AutoSize = true;
        this.lblMasterPassword.Location = new Point(30, 40);
        this.lblMasterPassword.Name = "lblMasterPassword";
        this.lblMasterPassword.Size = new Size(220, 19);
        this.lblMasterPassword.Text = "🔑 Ana Şifre (Master Password):";
        this.lblMasterPassword.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblMasterPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtMasterPassword
        this.txtMasterPassword.Location = new Point(30, 70);
        this.txtMasterPassword.Name = "txtMasterPassword";
        this.txtMasterPassword.PasswordChar = '●';
        this.txtMasterPassword.Size = new Size(340, 25);
        this.txtMasterPassword.TabIndex = 0;
        this.txtMasterPassword.BorderStyle = BorderStyle.FixedSingle;
        this.txtMasterPassword.BackColor = Color.White;
        this.txtMasterPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // btnOK
        this.btnOK.Location = new Point(280, 110);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(90, 40);
        this.btnOK.TabIndex = 1;
        this.btnOK.Text = "Tamam";
        this.btnOK.BackColor = Color.FromArgb(74, 144, 226);
        this.btnOK.ForeColor = Color.White;
        this.btnOK.FlatStyle = FlatStyle.Flat;
        this.btnOK.FlatAppearance.BorderSize = 0;
        this.btnOK.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnOK.Cursor = Cursors.Hand;
        this.btnOK.Click += BtnOK_Click;
        this.btnOK.MouseEnter += (s, e) => { this.btnOK.BackColor = Color.FromArgb(65, 130, 200); };
        this.btnOK.MouseLeave += (s, e) => { this.btnOK.BackColor = Color.FromArgb(74, 144, 226); };

        // MasterPasswordForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(400, 180);
        this.Controls.Add(this.btnOK);
        this.Controls.Add(this.txtMasterPassword);
        this.Controls.Add(this.lblMasterPassword);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "MasterPasswordForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "🔑 Ana Şifre";
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

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtMasterPassword.Text))
        {
            MessageBox.Show("Lütfen ana şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MasterPassword = txtMasterPassword.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
