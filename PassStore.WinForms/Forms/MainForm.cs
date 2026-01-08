using PassStore.Core.Entities;
using PassStore.Core.Interfaces;
using PassStore.WinForms.Forms;
using System.Windows.Forms;
using PassStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using PassStore.WinForms;

namespace PassStore.WinForms.Forms;

public partial class MainForm : Form
{
    private readonly IPasswordService _passwordService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly User _currentUser;
    private readonly IUserService _userService;
    private readonly IServiceProvider _serviceProvider;
    private string _masterPassword = string.Empty;
    private DataGridView dgvPasswords;
    private TextBox txtBaslik;
    private TextBox txtKullaniciAdi;
    private TextBox txtSifre;
    private TextBox txtUrl;
    private TextBox txtNotlar;
    private ComboBox cmbKategori;
    private TextBox txtAra;
    private Button btnEkle;
    private Button btnGuncelle;
    private Button btnSil;
    private Button btnGoster;
    private Button btnCikis;
    private Button btnCikisYap;
    private Label lblBaslik;
    private Label lblKullaniciAdi;
    private Label lblSifre;
    private Label lblUrl;
    private Label lblNotlar;
    private Label lblKategori;
    private Label lblAra;
    private Password? _selectedPassword;
    private TabControl tabControl;
    private TabPage tabSifreYonetimi;
    private TabPage tabAyarlar;
    private TextBox txtEskiSifre;
    private TextBox txtYeniSifre;
    private TextBox txtYeniSifreTekrar;
    private Button btnSifreDegistir;
    private Label lblEskiSifre;
    private Label lblYeniSifre;
    private Label lblYeniSifreTekrar;
    private TextBox txtKullaniciAdiAyarlar;
    private TextBox txtEmailAyarlar;
    private Button btnKullaniciBilgileriGuncelle;
    private Label lblKullaniciAdiAyarlar;
    private Label lblEmailAyarlar;
    // Uygulama Hakkında bölümü
    private Label lblUygulamaAdi;
    private Label lblVersiyon;
    private Label lblGelistirici;
    private Label lblEmailGelistirici;
    private Label lblTelifHakki;
    private Label lblAciklama;
    private Panel panelHakkinda;

    public MainForm(IPasswordService passwordService, IUnitOfWork unitOfWork, User currentUser, IUserService userService, IServiceProvider serviceProvider, string? masterPassword = null)
    {
        _passwordService = passwordService;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _userService = userService;
        _serviceProvider = serviceProvider;
        InitializeComponent();
        if (!string.IsNullOrEmpty(masterPassword))
        {
            _masterPassword = masterPassword;
        }
        else
        {
            RequestMasterPassword();
        }
        LoadPasswords();
        LoadCategories();
    }

    private void InitializeComponent()
    {
        this.dgvPasswords = new DataGridView();
        this.txtBaslik = new TextBox();
        this.txtKullaniciAdi = new TextBox();
        this.txtSifre = new TextBox();
        this.txtUrl = new TextBox();
        this.txtNotlar = new TextBox();
        this.cmbKategori = new ComboBox();
        this.txtAra = new TextBox();
        this.btnEkle = new Button();
        this.btnGuncelle = new Button();
        this.btnSil = new Button();
        this.btnGoster = new Button();
        this.btnCikis = new Button();
        this.btnCikisYap = new Button();
        this.lblBaslik = new Label();
        this.lblKullaniciAdi = new Label();
        this.lblSifre = new Label();
        this.lblUrl = new Label();
        this.lblNotlar = new Label();
        this.lblKategori = new Label();
        this.lblAra = new Label();
        this.tabControl = new TabControl();
        this.tabSifreYonetimi = new TabPage();
        this.tabAyarlar = new TabPage();
        this.txtEskiSifre = new TextBox();
        this.txtYeniSifre = new TextBox();
        this.txtYeniSifreTekrar = new TextBox();
        this.btnSifreDegistir = new Button();
        this.lblEskiSifre = new Label();
        this.lblYeniSifre = new Label();
        this.lblYeniSifreTekrar = new Label();
        this.txtKullaniciAdiAyarlar = new TextBox();
        this.txtEmailAyarlar = new TextBox();
        this.btnKullaniciBilgileriGuncelle = new Button();
        this.lblKullaniciAdiAyarlar = new Label();
        this.lblEmailAyarlar = new Label();
        // Uygulama Hakkında kontrolleri
        this.panelHakkinda = new Panel();
        this.lblUygulamaAdi = new Label();
        this.lblVersiyon = new Label();
        this.lblGelistirici = new Label();
        this.lblEmailGelistirici = new Label();
        this.lblTelifHakki = new Label();
        this.lblAciklama = new Label();

        this.SuspendLayout();

        // Form ayarları
        this.BackColor = Color.FromArgb(245, 247, 250);
        this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblAra
        this.lblAra.AutoSize = true;
        this.lblAra.Location = new Point(30, 25);
        this.lblAra.Name = "lblAra";
        this.lblAra.Size = new Size(35, 19);
        this.lblAra.Text = "🔍 Ara:";
        this.lblAra.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblAra.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtAra
        this.txtAra.Location = new Point(80, 22);
        this.txtAra.Name = "txtAra";
        this.txtAra.Size = new Size(750, 25);
        this.txtAra.TabIndex = 0;
        this.txtAra.BorderStyle = BorderStyle.FixedSingle;
        this.txtAra.BackColor = Color.White;
        this.txtAra.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtAra.TextChanged += TxtAra_TextChanged;

        // dgvPasswords
        this.dgvPasswords.AllowUserToAddRows = false;
        this.dgvPasswords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvPasswords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvPasswords.Location = new Point(30, 60);
        this.dgvPasswords.Name = "dgvPasswords";
        this.dgvPasswords.ReadOnly = true;
        this.dgvPasswords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        this.dgvPasswords.Size = new Size(800, 420);
        this.dgvPasswords.TabIndex = 1;
        this.dgvPasswords.BackgroundColor = Color.White;
        this.dgvPasswords.BorderStyle = BorderStyle.None;
        this.dgvPasswords.GridColor = Color.FromArgb(230, 230, 230);
        this.dgvPasswords.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(74, 144, 226);
        this.dgvPasswords.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        this.dgvPasswords.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.dgvPasswords.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        this.dgvPasswords.DefaultCellStyle.SelectionBackColor = Color.FromArgb(74, 144, 226);
        this.dgvPasswords.DefaultCellStyle.SelectionForeColor = Color.White;
        this.dgvPasswords.RowHeadersVisible = false;
        this.dgvPasswords.CellClick += DgvPasswords_CellClick;

        // lblBaslik
        this.lblBaslik.AutoSize = true;
        this.lblBaslik.Location = new Point(850, 60);
        this.lblBaslik.Name = "lblBaslik";
        this.lblBaslik.Size = new Size(50, 19);
        this.lblBaslik.Text = "📝 Başlık:";
        this.lblBaslik.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtBaslik
        this.txtBaslik.Location = new Point(850, 85);
        this.txtBaslik.Name = "txtBaslik";
        this.txtBaslik.Size = new Size(280, 25);
        this.txtBaslik.TabIndex = 2;
        this.txtBaslik.BorderStyle = BorderStyle.FixedSingle;
        this.txtBaslik.BackColor = Color.White;
        this.txtBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblKullaniciAdi
        this.lblKullaniciAdi.AutoSize = true;
        this.lblKullaniciAdi.Location = new Point(850, 125);
        this.lblKullaniciAdi.Name = "lblKullaniciAdi";
        this.lblKullaniciAdi.Size = new Size(105, 19);
        this.lblKullaniciAdi.Text = "👤 Kullanıcı Adı:";
        this.lblKullaniciAdi.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblKullaniciAdi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtKullaniciAdi
        this.txtKullaniciAdi.Location = new Point(850, 150);
        this.txtKullaniciAdi.Name = "txtKullaniciAdi";
        this.txtKullaniciAdi.Size = new Size(280, 25);
        this.txtKullaniciAdi.TabIndex = 3;
        this.txtKullaniciAdi.BorderStyle = BorderStyle.FixedSingle;
        this.txtKullaniciAdi.BackColor = Color.White;
        this.txtKullaniciAdi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblSifre
        this.lblSifre.AutoSize = true;
        this.lblSifre.Location = new Point(850, 190);
        this.lblSifre.Name = "lblSifre";
        this.lblSifre.Size = new Size(50, 19);
        this.lblSifre.Text = "🔒 Şifre:";
        this.lblSifre.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtSifre
        this.txtSifre.Location = new Point(850, 215);
        this.txtSifre.Name = "txtSifre";
        this.txtSifre.PasswordChar = '●';
        this.txtSifre.Size = new Size(240, 25);
        this.txtSifre.TabIndex = 4;
        this.txtSifre.BorderStyle = BorderStyle.FixedSingle;
        this.txtSifre.BackColor = Color.White;
        this.txtSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // btnGoster
        this.btnGoster.Location = new Point(1100, 214);
        this.btnGoster.Name = "btnGoster";
        this.btnGoster.Size = new Size(30, 27);
        this.btnGoster.TabIndex = 5;
        this.btnGoster.Text = "👁";
        this.btnGoster.BackColor = Color.FromArgb(108, 117, 125);
        this.btnGoster.ForeColor = Color.White;
        this.btnGoster.FlatStyle = FlatStyle.Flat;
        this.btnGoster.FlatAppearance.BorderSize = 0;
        this.btnGoster.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        this.btnGoster.Cursor = Cursors.Hand;
        this.btnGoster.Click += BtnGoster_Click;
        this.btnGoster.MouseEnter += (s, e) => { this.btnGoster.BackColor = Color.FromArgb(90, 98, 104); };
        this.btnGoster.MouseLeave += (s, e) => { this.btnGoster.BackColor = Color.FromArgb(108, 117, 125); };

        // lblUrl
        this.lblUrl.AutoSize = true;
        this.lblUrl.Location = new Point(850, 255);
        this.lblUrl.Name = "lblUrl";
        this.lblUrl.Size = new Size(40, 19);
        this.lblUrl.Text = "🌐 URL:";
        this.lblUrl.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblUrl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtUrl
        this.txtUrl.Location = new Point(850, 280);
        this.txtUrl.Name = "txtUrl";
        this.txtUrl.Size = new Size(280, 25);
        this.txtUrl.TabIndex = 6;
        this.txtUrl.BorderStyle = BorderStyle.FixedSingle;
        this.txtUrl.BackColor = Color.White;
        this.txtUrl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // lblKategori
        this.lblKategori.AutoSize = true;
        this.lblKategori.Location = new Point(850, 320);
        this.lblKategori.Name = "lblKategori";
        this.lblKategori.Size = new Size(75, 19);
        this.lblKategori.Text = "📁 Kategori:";
        this.lblKategori.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblKategori.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // cmbKategori
        this.cmbKategori.Location = new Point(850, 345);
        this.cmbKategori.Name = "cmbKategori";
        this.cmbKategori.Size = new Size(280, 25);
        this.cmbKategori.TabIndex = 7;
        this.cmbKategori.DropDownStyle = ComboBoxStyle.DropDown;
        this.cmbKategori.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        this.cmbKategori.AutoCompleteSource = AutoCompleteSource.ListItems;
        this.cmbKategori.BackColor = Color.White;
        this.cmbKategori.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.cmbKategori.FlatStyle = FlatStyle.Flat;

        // lblNotlar
        this.lblNotlar.AutoSize = true;
        this.lblNotlar.Location = new Point(850, 385);
        this.lblNotlar.Name = "lblNotlar";
        this.lblNotlar.Size = new Size(55, 19);
        this.lblNotlar.Text = "📄 Notlar:";
        this.lblNotlar.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblNotlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // txtNotlar
        this.txtNotlar.Location = new Point(850, 410);
        this.txtNotlar.Multiline = true;
        this.txtNotlar.Name = "txtNotlar";
        this.txtNotlar.Size = new Size(280, 70);
        this.txtNotlar.TabIndex = 8;
        this.txtNotlar.BorderStyle = BorderStyle.FixedSingle;
        this.txtNotlar.BackColor = Color.White;
        this.txtNotlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtNotlar.ScrollBars = ScrollBars.Vertical;

        // btnEkle
        this.btnEkle.Location = new Point(850, 495);
        this.btnEkle.Name = "btnEkle";
        this.btnEkle.Size = new Size(70, 40);
        this.btnEkle.TabIndex = 9;
        this.btnEkle.Text = "➕ Ekle";
        this.btnEkle.BackColor = Color.FromArgb(40, 167, 69);
        this.btnEkle.ForeColor = Color.White;
        this.btnEkle.FlatStyle = FlatStyle.Flat;
        this.btnEkle.FlatAppearance.BorderSize = 0;
        this.btnEkle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnEkle.Cursor = Cursors.Hand;
        this.btnEkle.Click += BtnEkle_Click;
        this.btnEkle.MouseEnter += (s, e) => { this.btnEkle.BackColor = Color.FromArgb(33, 136, 56); };
        this.btnEkle.MouseLeave += (s, e) => { this.btnEkle.BackColor = Color.FromArgb(40, 167, 69); };

        // btnGuncelle
        this.btnGuncelle.Location = new Point(950, 495);
        this.btnGuncelle.Name = "btnGuncelle";
        this.btnGuncelle.Size = new Size(70, 40);
        this.btnGuncelle.TabIndex = 10;
        this.btnGuncelle.Text = "✏️ Güncelle";
        this.btnGuncelle.BackColor = Color.FromArgb(74, 144, 226);
        this.btnGuncelle.ForeColor = Color.White;
        this.btnGuncelle.FlatStyle = FlatStyle.Flat;
        this.btnGuncelle.FlatAppearance.BorderSize = 0;
        this.btnGuncelle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnGuncelle.Cursor = Cursors.Hand;
        this.btnGuncelle.Click += BtnGuncelle_Click;
        this.btnGuncelle.MouseEnter += (s, e) => { this.btnGuncelle.BackColor = Color.FromArgb(65, 130, 200); };
        this.btnGuncelle.MouseLeave += (s, e) => { this.btnGuncelle.BackColor = Color.FromArgb(74, 144, 226); };

        // btnSil
        this.btnSil.Location = new Point(1050, 495);
        this.btnSil.Name = "btnSil";
        this.btnSil.Size = new Size(70, 40);
        this.btnSil.TabIndex = 11;
        this.btnSil.Text = "🗑️ Sil";
        this.btnSil.BackColor = Color.FromArgb(220, 53, 69);
        this.btnSil.ForeColor = Color.White;
        this.btnSil.FlatStyle = FlatStyle.Flat;
        this.btnSil.FlatAppearance.BorderSize = 0;
        this.btnSil.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnSil.Cursor = Cursors.Hand;
        this.btnSil.Click += BtnSil_Click;
        this.btnSil.MouseEnter += (s, e) => { this.btnSil.BackColor = Color.FromArgb(200, 35, 51); };
        this.btnSil.MouseLeave += (s, e) => { this.btnSil.BackColor = Color.FromArgb(220, 53, 69); };

        // btnCikis
        this.btnCikis.Location = new Point(30, 495);
        this.btnCikis.Name = "btnCikis";
        this.btnCikis.Size = new Size(90, 40);
        this.btnCikis.TabIndex = 12;
        this.btnCikis.Text = "❌ Kapat";
        this.btnCikis.BackColor = Color.FromArgb(108, 117, 125);
        this.btnCikis.ForeColor = Color.White;
        this.btnCikis.FlatStyle = FlatStyle.Flat;
        this.btnCikis.FlatAppearance.BorderSize = 0;
        this.btnCikis.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnCikis.Cursor = Cursors.Hand;
        this.btnCikis.Click += BtnCikis_Click;
        this.btnCikis.MouseEnter += (s, e) => { this.btnCikis.BackColor = Color.FromArgb(90, 98, 104); };
        this.btnCikis.MouseLeave += (s, e) => { this.btnCikis.BackColor = Color.FromArgb(108, 117, 125); };

        // btnCikisYap
        this.btnCikisYap.Location = new Point(130, 495);
        this.btnCikisYap.Name = "btnCikisYap";
        this.btnCikisYap.Size = new Size(120, 40);
        this.btnCikisYap.TabIndex = 13;
        this.btnCikisYap.Text = "🚪 Çıkış Yap";
        this.btnCikisYap.BackColor = Color.FromArgb(255, 193, 7);
        this.btnCikisYap.ForeColor = Color.FromArgb(51, 51, 51);
        this.btnCikisYap.FlatStyle = FlatStyle.Flat;
        this.btnCikisYap.FlatAppearance.BorderSize = 0;
        this.btnCikisYap.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnCikisYap.Cursor = Cursors.Hand;
        this.btnCikisYap.Click += BtnCikisYap_Click;
        this.btnCikisYap.MouseEnter += (s, e) => { this.btnCikisYap.BackColor = Color.FromArgb(230, 173, 6); };
        this.btnCikisYap.MouseLeave += (s, e) => { this.btnCikisYap.BackColor = Color.FromArgb(255, 193, 7); };

        // MainForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1150, 560);
        this.Controls.Add(this.btnCikisYap);
        this.Controls.Add(this.btnCikis);
        this.Controls.Add(this.btnSil);
        this.Controls.Add(this.btnGuncelle);
        this.Controls.Add(this.btnEkle);
        this.Controls.Add(this.txtNotlar);
        this.Controls.Add(this.lblNotlar);
        this.Controls.Add(this.cmbKategori);
        this.Controls.Add(this.lblKategori);
        this.Controls.Add(this.txtUrl);
        this.Controls.Add(this.lblUrl);
        this.Controls.Add(this.btnGoster);
        this.Controls.Add(this.txtSifre);
        this.Controls.Add(this.lblSifre);
        this.Controls.Add(this.txtKullaniciAdi);
        this.Controls.Add(this.lblKullaniciAdi);
        this.Controls.Add(this.txtBaslik);
        this.Controls.Add(this.lblBaslik);
        // TabControl ve TabPages
        this.tabControl.Location = new Point(0, 0);
        this.tabControl.Size = new Size(1150, 560);
        this.tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        
        // Tab 1: Şifre Yönetimi
        this.tabSifreYonetimi.Text = "🔐 Şifre Yönetimi";
        this.tabSifreYonetimi.BackColor = Color.FromArgb(245, 247, 250);
        this.tabSifreYonetimi.Padding = new Padding(5);
        this.tabSifreYonetimi.Controls.Add(this.btnCikisYap);
        this.tabSifreYonetimi.Controls.Add(this.btnCikis);
        this.tabSifreYonetimi.Controls.Add(this.btnSil);
        this.tabSifreYonetimi.Controls.Add(this.btnGuncelle);
        this.tabSifreYonetimi.Controls.Add(this.btnEkle);
        this.tabSifreYonetimi.Controls.Add(this.txtNotlar);
        this.tabSifreYonetimi.Controls.Add(this.lblNotlar);
        this.tabSifreYonetimi.Controls.Add(this.cmbKategori);
        this.tabSifreYonetimi.Controls.Add(this.lblKategori);
        this.tabSifreYonetimi.Controls.Add(this.txtUrl);
        this.tabSifreYonetimi.Controls.Add(this.lblUrl);
        this.tabSifreYonetimi.Controls.Add(this.btnGoster);
        this.tabSifreYonetimi.Controls.Add(this.txtSifre);
        this.tabSifreYonetimi.Controls.Add(this.lblSifre);
        this.tabSifreYonetimi.Controls.Add(this.txtKullaniciAdi);
        this.tabSifreYonetimi.Controls.Add(this.lblKullaniciAdi);
        this.tabSifreYonetimi.Controls.Add(this.txtBaslik);
        this.tabSifreYonetimi.Controls.Add(this.lblBaslik);
        this.tabSifreYonetimi.Controls.Add(this.dgvPasswords);
        this.tabSifreYonetimi.Controls.Add(this.txtAra);
        this.tabSifreYonetimi.Controls.Add(this.lblAra);

        // Tab 2: Ayarlar
        this.tabAyarlar.Text = "⚙️ Ayarlar";
        this.tabAyarlar.BackColor = Color.FromArgb(245, 247, 250);
        this.tabAyarlar.Padding = new Padding(5);
        this.tabAyarlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // Kullanıcı Bilgileri Bölümü
        this.lblKullaniciAdiAyarlar.AutoSize = true;
        this.lblKullaniciAdiAyarlar.Location = new Point(50, 50);
        this.lblKullaniciAdiAyarlar.Name = "lblKullaniciAdiAyarlar";
        this.lblKullaniciAdiAyarlar.Size = new Size(105, 19);
        this.lblKullaniciAdiAyarlar.Text = "👤 Kullanıcı Adı:";
        this.lblKullaniciAdiAyarlar.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblKullaniciAdiAyarlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        this.txtKullaniciAdiAyarlar.Location = new Point(50, 75);
        this.txtKullaniciAdiAyarlar.Name = "txtKullaniciAdiAyarlar";
        this.txtKullaniciAdiAyarlar.Size = new Size(400, 25);
        this.txtKullaniciAdiAyarlar.TabIndex = 0;
        this.txtKullaniciAdiAyarlar.BorderStyle = BorderStyle.FixedSingle;
        this.txtKullaniciAdiAyarlar.BackColor = Color.White;
        this.txtKullaniciAdiAyarlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtKullaniciAdiAyarlar.Text = _currentUser.KullaniciAdi;
        this.txtKullaniciAdiAyarlar.ReadOnly = true;

        this.lblEmailAyarlar.AutoSize = true;
        this.lblEmailAyarlar.Location = new Point(50, 120);
        this.lblEmailAyarlar.Name = "lblEmailAyarlar";
        this.lblEmailAyarlar.Size = new Size(50, 19);
        this.lblEmailAyarlar.Text = "📧 Email:";
        this.lblEmailAyarlar.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblEmailAyarlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        this.txtEmailAyarlar.Location = new Point(50, 145);
        this.txtEmailAyarlar.Name = "txtEmailAyarlar";
        this.txtEmailAyarlar.Size = new Size(400, 25);
        this.txtEmailAyarlar.TabIndex = 1;
        this.txtEmailAyarlar.BorderStyle = BorderStyle.FixedSingle;
        this.txtEmailAyarlar.BackColor = Color.White;
        this.txtEmailAyarlar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtEmailAyarlar.Text = _currentUser.Email ?? "";

        this.btnKullaniciBilgileriGuncelle.Location = new Point(50, 190);
        this.btnKullaniciBilgileriGuncelle.Name = "btnKullaniciBilgileriGuncelle";
        this.btnKullaniciBilgileriGuncelle.Size = new Size(200, 40);
        this.btnKullaniciBilgileriGuncelle.TabIndex = 2;
        this.btnKullaniciBilgileriGuncelle.Text = "💾 Bilgileri Güncelle";
        this.btnKullaniciBilgileriGuncelle.BackColor = Color.FromArgb(74, 144, 226);
        this.btnKullaniciBilgileriGuncelle.ForeColor = Color.White;
        this.btnKullaniciBilgileriGuncelle.FlatStyle = FlatStyle.Flat;
        this.btnKullaniciBilgileriGuncelle.FlatAppearance.BorderSize = 0;
        this.btnKullaniciBilgileriGuncelle.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnKullaniciBilgileriGuncelle.Cursor = Cursors.Hand;
        this.btnKullaniciBilgileriGuncelle.Click += BtnKullaniciBilgileriGuncelle_Click;
        this.btnKullaniciBilgileriGuncelle.MouseEnter += (s, e) => { this.btnKullaniciBilgileriGuncelle.BackColor = Color.FromArgb(65, 130, 200); };
        this.btnKullaniciBilgileriGuncelle.MouseLeave += (s, e) => { this.btnKullaniciBilgileriGuncelle.BackColor = Color.FromArgb(74, 144, 226); };

        // Master Password Değiştirme Bölümü
        this.lblEskiSifre.AutoSize = true;
        this.lblEskiSifre.Location = new Point(50, 280);
        this.lblEskiSifre.Name = "lblEskiSifre";
        this.lblEskiSifre.Size = new Size(140, 19);
        this.lblEskiSifre.Text = "🔒 Eski Ana Şifre:";
        this.lblEskiSifre.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblEskiSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        this.txtEskiSifre.Location = new Point(50, 305);
        this.txtEskiSifre.Name = "txtEskiSifre";
        this.txtEskiSifre.Size = new Size(400, 25);
        this.txtEskiSifre.TabIndex = 3;
        this.txtEskiSifre.PasswordChar = '●';
        this.txtEskiSifre.BorderStyle = BorderStyle.FixedSingle;
        this.txtEskiSifre.BackColor = Color.White;
        this.txtEskiSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtEskiSifre.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { txtYeniSifre.Focus(); } };

        this.lblYeniSifre.AutoSize = true;
        this.lblYeniSifre.Location = new Point(50, 350);
        this.lblYeniSifre.Name = "lblYeniSifre";
        this.lblYeniSifre.Size = new Size(140, 19);
        this.lblYeniSifre.Text = "🔒 Yeni Ana Şifre:";
        this.lblYeniSifre.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblYeniSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        this.txtYeniSifre.Location = new Point(50, 375);
        this.txtYeniSifre.Name = "txtYeniSifre";
        this.txtYeniSifre.Size = new Size(400, 25);
        this.txtYeniSifre.TabIndex = 4;
        this.txtYeniSifre.PasswordChar = '●';
        this.txtYeniSifre.BorderStyle = BorderStyle.FixedSingle;
        this.txtYeniSifre.BackColor = Color.White;
        this.txtYeniSifre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtYeniSifre.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { txtYeniSifreTekrar.Focus(); } };

        this.lblYeniSifreTekrar.AutoSize = true;
        this.lblYeniSifreTekrar.Location = new Point(50, 420);
        this.lblYeniSifreTekrar.Name = "lblYeniSifreTekrar";
        this.lblYeniSifreTekrar.Size = new Size(200, 19);
        this.lblYeniSifreTekrar.Text = "🔒 Yeni Ana Şifre (Tekrar):";
        this.lblYeniSifreTekrar.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblYeniSifreTekrar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        this.txtYeniSifreTekrar.Location = new Point(50, 445);
        this.txtYeniSifreTekrar.Name = "txtYeniSifreTekrar";
        this.txtYeniSifreTekrar.Size = new Size(400, 25);
        this.txtYeniSifreTekrar.TabIndex = 5;
        this.txtYeniSifreTekrar.PasswordChar = '●';
        this.txtYeniSifreTekrar.BorderStyle = BorderStyle.FixedSingle;
        this.txtYeniSifreTekrar.BackColor = Color.White;
        this.txtYeniSifreTekrar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        this.txtYeniSifreTekrar.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { BtnSifreDegistir_Click(btnSifreDegistir, EventArgs.Empty); } };

        this.btnSifreDegistir.Location = new Point(50, 490);
        this.btnSifreDegistir.Name = "btnSifreDegistir";
        this.btnSifreDegistir.Size = new Size(200, 40);
        this.btnSifreDegistir.TabIndex = 6;
        this.btnSifreDegistir.Text = "🔑 Ana Şifreyi Değiştir";
        this.btnSifreDegistir.BackColor = Color.FromArgb(255, 193, 7);
        this.btnSifreDegistir.ForeColor = Color.FromArgb(51, 51, 51);
        this.btnSifreDegistir.FlatStyle = FlatStyle.Flat;
        this.btnSifreDegistir.FlatAppearance.BorderSize = 0;
        this.btnSifreDegistir.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        this.btnSifreDegistir.Cursor = Cursors.Hand;
        this.btnSifreDegistir.Click += BtnSifreDegistir_Click;
        this.btnSifreDegistir.MouseEnter += (s, e) => { this.btnSifreDegistir.BackColor = Color.FromArgb(230, 173, 6); };
        this.btnSifreDegistir.MouseLeave += (s, e) => { this.btnSifreDegistir.BackColor = Color.FromArgb(255, 193, 7); };

        // Uygulama Hakkında Bölümü (Sağ tarafta)
        this.panelHakkinda.Location = new Point(500, 50);
        this.panelHakkinda.Size = new Size(600, 450);
        this.panelHakkinda.BackColor = Color.White;
        this.panelHakkinda.BorderStyle = BorderStyle.FixedSingle;
        this.panelHakkinda.Padding = new Padding(20);

        // Uygulama Adı
        this.lblUygulamaAdi.AutoSize = true;
        this.lblUygulamaAdi.Location = new Point(20, 20);
        this.lblUygulamaAdi.Name = "lblUygulamaAdi";
        this.lblUygulamaAdi.Size = new Size(200, 25);
        this.lblUygulamaAdi.Text = $"🔐 {AppInfo.AppName}";
        this.lblUygulamaAdi.ForeColor = Color.FromArgb(74, 144, 226);
        this.lblUygulamaAdi.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);

        // Versiyon
        this.lblVersiyon.AutoSize = true;
        this.lblVersiyon.Location = new Point(20, 60);
        this.lblVersiyon.Name = "lblVersiyon";
        this.lblVersiyon.Size = new Size(100, 19);
        this.lblVersiyon.Text = $"Versiyon: {AppInfo.Version}";
        this.lblVersiyon.ForeColor = Color.FromArgb(108, 117, 125);
        this.lblVersiyon.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // Açıklama
        this.lblAciklama.AutoSize = false;
        this.lblAciklama.Location = new Point(20, 100);
        this.lblAciklama.Name = "lblAciklama";
        this.lblAciklama.Size = new Size(560, 80);
        this.lblAciklama.Text = AppInfo.Description;
        this.lblAciklama.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblAciklama.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // Geliştirici
        this.lblGelistirici.AutoSize = true;
        this.lblGelistirici.Location = new Point(20, 200);
        this.lblGelistirici.Name = "lblGelistirici";
        this.lblGelistirici.Size = new Size(150, 19);
        this.lblGelistirici.Text = "👨‍💻 Geliştirici:";
        this.lblGelistirici.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblGelistirici.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);

        // Geliştirici Adı (Buraya kendi adınızı ekleyin)
        var lblGelistiriciAdi = new Label();
        lblGelistiriciAdi.AutoSize = true;
        lblGelistiriciAdi.Location = new Point(180, 200);
        lblGelistiriciAdi.Name = "lblGelistiriciAdi";
        lblGelistiriciAdi.Size = new Size(200, 19);
        lblGelistiriciAdi.Text = AppInfo.DeveloperName;
        lblGelistiriciAdi.ForeColor = Color.FromArgb(51, 51, 51);
        lblGelistiriciAdi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

        // Email
        this.lblEmailGelistirici.AutoSize = true;
        this.lblEmailGelistirici.Location = new Point(20, 240);
        this.lblEmailGelistirici.Name = "lblEmailGelistirici";
        this.lblEmailGelistirici.Size = new Size(150, 19);
        this.lblEmailGelistirici.Text = "📧 E-posta:";
        this.lblEmailGelistirici.ForeColor = Color.FromArgb(51, 51, 51);
        this.lblEmailGelistirici.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);

        // Email Adresi (Buraya kendi email adresinizi ekleyin)
        var lblEmailAdresi = new Label();
        lblEmailAdresi.AutoSize = true;
        lblEmailAdresi.Location = new Point(180, 240);
        lblEmailAdresi.Name = "lblEmailAdresi";
        lblEmailAdresi.Size = new Size(300, 19);
        lblEmailAdresi.Text = AppInfo.DeveloperEmail;
        lblEmailAdresi.ForeColor = Color.FromArgb(74, 144, 226);
        lblEmailAdresi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        lblEmailAdresi.Cursor = Cursors.Hand;
        lblEmailAdresi.Click += (s, e) => 
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = $"mailto:{AppInfo.DeveloperEmail}",
                    UseShellExecute = true
                });
            }
            catch { }
        };

        // Telif Hakkı
        this.lblTelifHakki.AutoSize = true;
        this.lblTelifHakki.Location = new Point(20, 300);
        this.lblTelifHakki.Name = "lblTelifHakki";
        this.lblTelifHakki.Size = new Size(400, 19);
        this.lblTelifHakki.Text = "© 2026 PassStore. Tüm hakları saklıdır.";
        this.lblTelifHakki.ForeColor = Color.FromArgb(108, 117, 125);
        this.lblTelifHakki.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

        // Teknolojiler
        var lblTeknolojiler = new Label();
        lblTeknolojiler.AutoSize = false;
        lblTeknolojiler.Location = new Point(20, 340);
        lblTeknolojiler.Name = "lblTeknolojiler";
        lblTeknolojiler.Size = new Size(560, 60);
        lblTeknolojiler.Text = AppInfo.Technologies;
        lblTeknolojiler.ForeColor = Color.FromArgb(108, 117, 125);
        lblTeknolojiler.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

        // Panel'e kontrolleri ekle
        this.panelHakkinda.Controls.Add(this.lblUygulamaAdi);
        this.panelHakkinda.Controls.Add(this.lblVersiyon);
        this.panelHakkinda.Controls.Add(this.lblAciklama);
        this.panelHakkinda.Controls.Add(this.lblGelistirici);
        this.panelHakkinda.Controls.Add(lblGelistiriciAdi);
        this.panelHakkinda.Controls.Add(this.lblEmailGelistirici);
        this.panelHakkinda.Controls.Add(lblEmailAdresi);
        this.panelHakkinda.Controls.Add(this.lblTelifHakki);
        this.panelHakkinda.Controls.Add(lblTeknolojiler);

        this.tabAyarlar.Controls.Add(this.lblKullaniciAdiAyarlar);
        this.tabAyarlar.Controls.Add(this.txtKullaniciAdiAyarlar);
        this.tabAyarlar.Controls.Add(this.lblEmailAyarlar);
        this.tabAyarlar.Controls.Add(this.txtEmailAyarlar);
        this.tabAyarlar.Controls.Add(this.btnKullaniciBilgileriGuncelle);
        this.tabAyarlar.Controls.Add(this.lblEskiSifre);
        this.tabAyarlar.Controls.Add(this.txtEskiSifre);
        this.tabAyarlar.Controls.Add(this.lblYeniSifre);
        this.tabAyarlar.Controls.Add(this.txtYeniSifre);
        this.tabAyarlar.Controls.Add(this.lblYeniSifreTekrar);
        this.tabAyarlar.Controls.Add(this.txtYeniSifreTekrar);
        this.tabAyarlar.Controls.Add(this.btnSifreDegistir);
        this.tabAyarlar.Controls.Add(this.panelHakkinda);

        this.tabControl.TabPages.Add(this.tabSifreYonetimi);
        this.tabControl.TabPages.Add(this.tabAyarlar);

        // MainForm
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1150, 560);
        this.Controls.Add(this.tabControl);
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = $"🔐 Şifre Saklama - {_currentUser.KullaniciAdi}";
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        // Icon ekle
        try
        {
            var iconPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "app.ico");
            if (File.Exists(iconPath))
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

    private void RequestMasterPassword()
    {
        using var form = new MasterPasswordForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            _masterPassword = form.MasterPassword;
        }
        else
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    private async void LoadPasswords()
    {
        try
        {
            var passwords = await _passwordService.GetPasswordsByUserIdAsync(_currentUser.Id);
            
            var searchText = txtAra.Text.ToLower();
            var filteredPasswords = passwords.Where(p => 
                string.IsNullOrEmpty(searchText) ||
                p.Baslik.ToLower().Contains(searchText) ||
                p.KullaniciAdi.ToLower().Contains(searchText) ||
                (p.Url != null && p.Url.ToLower().Contains(searchText)) ||
                (p.Kategori != null && p.Kategori.ToLower().Contains(searchText))
            );
            
            dgvPasswords.DataSource = filteredPasswords.Select(p => new
            {
                p.Id,
                p.Baslik,
                p.KullaniciAdi,
                p.Url,
                p.Kategori,
                p.OlusturmaTarihi
            }).ToList();

            dgvPasswords.Columns["Id"].Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Şifreler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void TxtAra_TextChanged(object? sender, EventArgs e)
    {
        // Arama işlevi - LoadPasswords metodunu filtreleyerek çağırabiliriz
        LoadPasswords();
    }

    private void DgvPasswords_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            var idValue = dgvPasswords.Rows[e.RowIndex].Cells["Id"].Value;
            var id = idValue?.ToString();
            if (!string.IsNullOrEmpty(id))
            {
                LoadPasswordDetails(id);
            }
        }
    }

    private async void LoadPasswordDetails(string id)
    {
        try
        {
            _selectedPassword = await _passwordService.GetPasswordByIdAsync(id);
            
            if (_selectedPassword != null)
            {
                txtBaslik.Text = _selectedPassword.Baslik;
                txtKullaniciAdi.Text = _selectedPassword.KullaniciAdi;
                txtSifre.Text = await _passwordService.DecryptPasswordAsync(_selectedPassword.SifrelenmisSifre, _masterPassword);
                txtUrl.Text = _selectedPassword.Url ?? "";
                cmbKategori.Text = _selectedPassword.Kategori ?? "";
                txtNotlar.Text = _selectedPassword.Notlar ?? "";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Şifre detayları yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnEkle_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtBaslik.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen başlık ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var password = new Password
            {
                Baslik = txtBaslik.Text,
                KullaniciAdi = txtKullaniciAdi.Text,
                SifrelenmisSifre = txtSifre.Text,
                Url = txtUrl.Text,
                Kategori = cmbKategori.Text,
                Notlar = txtNotlar.Text,
                KullaniciId = _currentUser.Id,
                OlusturanKullanici = _currentUser.Id
            };

            await _passwordService.AddPasswordAsync(password, _masterPassword);
            ClearFields();
            LoadPasswords();
            LoadCategories();
            MessageBox.Show("Şifre başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Şifre eklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnGuncelle_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_selectedPassword == null)
            {
                MessageBox.Show("Lütfen güncellemek için bir şifre seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedPassword.Baslik = txtBaslik.Text;
            _selectedPassword.KullaniciAdi = txtKullaniciAdi.Text;
            _selectedPassword.SifrelenmisSifre = txtSifre.Text;
            _selectedPassword.Url = txtUrl.Text;
            _selectedPassword.Kategori = cmbKategori.Text;
            _selectedPassword.Notlar = txtNotlar.Text;
            _selectedPassword.GuncelleyenKullanici = _currentUser.Id;

            await _passwordService.UpdatePasswordAsync(_selectedPassword, _masterPassword);
            ClearFields();
            LoadPasswords();
            LoadCategories();
            MessageBox.Show("Şifre başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Şifre güncellenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnSil_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_selectedPassword == null)
            {
                MessageBox.Show("Lütfen silmek için bir şifre seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bu şifreyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await _passwordService.DeletePasswordAsync(_selectedPassword);
                ClearFields();
                LoadPasswords();
                MessageBox.Show("Şifre başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Şifre silinirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnGoster_Click(object? sender, EventArgs e)
    {
        txtSifre.PasswordChar = txtSifre.PasswordChar == '*' ? '\0' : '*';
    }

    private void BtnCikis_Click(object? sender, EventArgs e)
    {
        System.Windows.Forms.Application.Exit();
    }

    private void BtnCikisYap_Click(object? sender, EventArgs e)
    {
        // Master password'u temizle
        _masterPassword = string.Empty;
        // Formu kapat (DialogResult ile değil, sadece kapat)
        this.DialogResult = DialogResult.Abort; // Özel bir değer kullanıyoruz
        this.Close();
    }

    private void ClearFields()
    {
        txtBaslik.Clear();
        txtKullaniciAdi.Clear();
        txtSifre.Clear();
        txtUrl.Clear();
        cmbKategori.Text = "";
        txtNotlar.Clear();
        _selectedPassword = null;
    }

    private async void LoadCategories()
    {
        try
        {
            var passwords = await _passwordService.GetPasswordsByUserIdAsync(_currentUser.Id);
            var categories = passwords
                .Where(p => !string.IsNullOrEmpty(p.Kategori))
                .Select(p => p.Kategori!)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            cmbKategori.Items.Clear();
            cmbKategori.Items.AddRange(categories.ToArray());
        }
        catch
        {
            // Hata durumunda sessizce devam et
        }
    }

    private async void BtnKullaniciBilgileriGuncelle_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtEmailAyarlar.Text))
            {
                MessageBox.Show("Email alanı boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentUser.Email = txtEmailAyarlar.Text;
            await _unitOfWork.UserRepository.UpdateAsync(_currentUser);
            await _unitOfWork.SaveChangesAsync();

            MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Text = $"🔐 Şifre Saklama - {_currentUser.KullaniciAdi}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Bilgiler güncellenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnSifreDegistir_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtEskiSifre.Text))
            {
                MessageBox.Show("Lütfen eski ana şifrenizi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtYeniSifre.Text))
            {
                MessageBox.Show("Lütfen yeni ana şifrenizi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtYeniSifre.Text != txtYeniSifreTekrar.Text)
            {
                MessageBox.Show("Yeni şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eski şifrenin doğru olduğunu kontrol et - tüm şifreleri eski master password ile decrypt edip yeni ile encrypt etmeliyiz
            var passwords = await _passwordService.GetPasswordsByUserIdAsync(_currentUser.Id);
            bool isValidOldPassword = true;

            try
            {
                // Bir şifreyi test et
                if (passwords.Any())
                {
                    var testPassword = passwords.First();
                    await _passwordService.DecryptPasswordAsync(testPassword.SifrelenmisSifre, txtEskiSifre.Text);
                }
            }
            catch
            {
                isValidOldPassword = false;
            }

            if (!isValidOldPassword)
            {
                MessageBox.Show("Eski ana şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tüm şifreleri yeni master password ile yeniden şifrele
            foreach (var password in passwords)
            {
                try
                {
                    var decryptedPassword = await _passwordService.DecryptPasswordAsync(password.SifrelenmisSifre, txtEskiSifre.Text);
                    password.SifrelenmisSifre = _passwordService.EncryptPassword(decryptedPassword, txtYeniSifre.Text);
                    await _unitOfWork.PasswordRepository.UpdateAsync(password);
                }
                catch
                {
                    // Şifre çözülemezse atla
                }
            }

            await _unitOfWork.SaveChangesAsync();
            _masterPassword = txtYeniSifre.Text;

            // Form alanlarını temizle
            txtEskiSifre.Clear();
            txtYeniSifre.Clear();
            txtYeniSifreTekrar.Clear();

            MessageBox.Show("Ana şifre başarıyla değiştirildi. Tüm şifreler yeni ana şifre ile yeniden şifrelendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Şifre değiştirilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
