# PassStore Kurulum Dosyası Oluşturma Rehberi

Bu rehber, PassStore uygulaması için kurulum dosyası (installer) oluşturma adımlarını içerir.

## Kurulum Türleri

### 1. Framework-Dependent (Küçük Boyut)
- .NET 8.0 Runtime gerektirir
- Daha küçük kurulum dosyası (~5-10 MB)
- Kullanıcıların .NET Runtime yüklemesi gerekir

### 2. Self-Contained (Tek Dosya)
- .NET Runtime gerektirmez
- Daha büyük kurulum dosyası (~50-100 MB)
- Tüm bağımlılıklar dahil
- Herhangi bir Windows bilgisayarda çalışır

## Yöntem 1: Inno Setup (Önerilen)

### Adım 1: Inno Setup'u İndirin ve Yükleyin

1. https://jrsoftware.org/isinfo.php adresinden Inno Setup'u indirin
2. İndirilen dosyayı çalıştırıp kurulumu tamamlayın
3. (Opsiyonel) PATH ortam değişkenine ekleyin

### Adım 2: Kurulum Dosyasını Oluşturun

#### Framework-Dependent Kurulum (Küçük):

**PowerShell ile:**
```powershell
cd PassStore.WinForms
.\build_installer.ps1
```

**Batch Dosyası ile:**
```cmd
cd PassStore.WinForms
build_installer.bat
```

#### Self-Contained Kurulum (Tek Dosya, .NET Gerektirmez):

**Batch Dosyası ile:**
```cmd
cd PassStore.WinForms
publish_and_install.bat
```

Bu komut:
1. Self-contained modda publish eder
2. Tüm bağımlılıkları içerir
3. Kurulum dosyasını oluşturur

#### Manuel Olarak:
1. Visual Studio veya başka bir editörle `installer.iss` dosyasını açın
2. Inno Setup Compiler'ı çalıştırın
3. `installer.iss` dosyasını açın
4. "Build" > "Compile" menüsünden derleyin

### Adım 3: Kurulum Dosyasını Bulun

**Framework-Dependent:**
```
PassStore\Installer\PassStore_Setup_1.0.0.exe
```

**Self-Contained:**
```
PassStore\Installer\PassStore_Setup_1.0.0_SelfContained.exe
```

## Yöntem 2: WiX Toolset (Gelişmiş)

WiX Toolset kullanmak isterseniz:

1. WiX Toolset'i indirin: https://wixtoolset.org/
2. Visual Studio'da WiX projesi oluşturun
3. Gerekli dosyaları ekleyin

## Kurulum Dosyası Özellikleri

- ✅ Modern kurulum sihirbazı
- ✅ Masaüstü kısayolu oluşturma seçeneği
- ✅ Başlat menüsüne ekleme
- ✅ .NET 8.0 Runtime kontrolü (Framework-Dependent versiyonda)
- ✅ Türkçe ve İngilizce dil desteği
- ✅ Otomatik güncelleme desteği (yapılandırılabilir)
- ✅ Kaldırma (Uninstall) desteği
- ✅ Self-contained seçeneği (.NET gerektirmez)

## Önemli Notlar

1. **Release Modu**: Kurulum dosyası oluşturmadan önce uygulamayı Release modunda derleyin
2. **.NET Runtime**: 
   - Framework-Dependent versiyon: Kullanıcıların .NET 8.0 Runtime'ı yüklü olması gerekir
   - Self-Contained versiyon: .NET Runtime gerektirmez (tüm bağımlılıklar dahil)
3. **Veritabanı**: İlk kurulumda veritabanı otomatik oluşturulur
4. **Icon**: `app.ico` dosyası kurulum dosyasında kullanılır
5. **Dosya Boyutu**: 
   - Framework-Dependent: ~5-10 MB
   - Self-Contained: ~50-100 MB

## Kurulum Dosyasını Özelleştirme

`installer.iss` dosyasını düzenleyerek:
- Uygulama adını değiştirebilirsiniz
- Versiyon numarasını güncelleyebilirsiniz
- Ek dosyalar ekleyebilirsiniz
- Kayıt defteri girdileri ekleyebilirsiniz
- Özel kurulum adımları ekleyebilirsiniz

## Sorun Giderme

### "Inno Setup bulunamadı" Hatası
- Inno Setup'un yüklü olduğundan emin olun
- PATH'e eklenmişse, yeni bir terminal açın
- Manuel olarak Inno Setup Compiler'ı kullanın

### ".NET Runtime bulunamadı" Hatası
- Kullanıcıların .NET 8.0 Runtime'ı yüklemesi gerekir
- Kurulum dosyası bu kontrolü otomatik yapar

### "Dosya bulunamadı" Hatası
- Önce Release modunda derleme yaptığınızdan emin olun
- `bin\Release\net8.0-windows` klasörünün var olduğunu kontrol edin

## Ek Kaynaklar

- Inno Setup Dokümantasyonu: https://jrsoftware.org/ishelp/
- Inno Setup Örnekleri: https://jrsoftware.org/isdl.php#examples
