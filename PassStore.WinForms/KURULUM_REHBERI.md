# PassStore Kurulum Dosyası Oluşturma - Hızlı Başlangıç

## 🚀 Hızlı Başlangıç

### 1. Inno Setup'u İndirin
https://jrsoftware.org/isinfo.php adresinden Inno Setup'u indirip yükleyin.

### 2. Kurulum Dosyasını Oluşturun

**PowerShell ile:**
```powershell
cd PassStore.WinForms
.\build_installer.ps1
```

**Veya Batch ile:**
```cmd
cd PassStore.WinForms
build_installer.bat
```

### 3. Kurulum Dosyasını Bulun
```
PassStore\Installer\PassStore_Setup_1.0.0.exe
```

## 📦 İki Farklı Kurulum Türü

### Seçenek 1: Framework-Dependent (Küçük - Önerilen)
- **Boyut**: ~5-10 MB
- **Gereksinim**: .NET 8.0 Runtime yüklü olmalı
- **Kullanım**: Çoğu kullanıcı için uygun
- **Komut**: `build_installer.bat` veya `build_installer.ps1`

### Seçenek 2: Self-Contained (Tek Dosya)
- **Boyut**: ~50-100 MB
- **Gereksinim**: .NET Runtime gerekmez
- **Kullanım**: .NET yüklü olmayan bilgisayarlar için
- **Komut**: `publish_and_install.bat`

## ⚙️ Kurulum Dosyası Özellikleri

✅ Modern kurulum sihirbazı  
✅ Masaüstü kısayolu seçeneği  
✅ Başlat menüsüne ekleme  
✅ Otomatik kaldırma desteği  
✅ Türkçe/İngilizce dil desteği  
✅ .NET Runtime kontrolü (Framework-Dependent)  

## 📝 Detaylı Bilgi

Daha fazla bilgi için `README_INSTALLER.md` dosyasına bakın.
