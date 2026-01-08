# 🔐 PassStore

PassStore, şifrelerinizi güvenli bir şekilde saklamanızı ve yönetmenizi sağlayan bir Windows Forms uygulamasıdır. Tüm şifreleriniz AES şifreleme ile korunur ve sadece ana şifreniz ile erişilebilir.

## ✨ Özellikler

- 🔒 **Güvenli Şifre Saklama**: AES şifreleme ile korumalı şifre depolama
- 👤 **Kullanıcı Yönetimi**: Kayıt ve giriş sistemi
- 🔑 **Ana Şifre Koruması**: Master password ile ekstra güvenlik katmanı
- 📁 **Kategori Desteği**: Şifrelerinizi kategorilere ayırabilirsiniz
- 🔍 **Arama Özelliği**: Hızlı şifre arama
- 💾 **Yerel Veritabanı**: SQLite ile yerel veri saklama
- 🎨 **Modern Arayüz**: Kullanıcı dostu ve modern tasarım
- ⚙️ **Ayarlar**: Şifre değiştirme ve kullanıcı bilgileri güncelleme
- 📋 **URL ve Notlar**: Her şifre için URL ve notlar ekleyebilirsiniz

## 🛠️ Teknolojiler

- **.NET 8.0** - Framework
- **Windows Forms** - UI Framework
- **SQLite** - Veritabanı
- **AES Şifreleme** - Güvenlik
- **FluentMigrator** - Veritabanı Migration
- **BCrypt** - Şifre Hashleme
- **Clean Architecture** - Mimari Yapı

## 📋 Gereksinimler

- Windows 10 veya üzeri
- .NET 8.0 Runtime (Framework-Dependent kurulum için)
- Veya Self-Contained kurulum (Runtime gerektirmez)

## 🚀 Kurulum

### Yöntem 1: Kurulum Dosyası ile (Önerilen)

1. [Releases](https://github.com/harunkececi/passStore/releases) sayfasından en son kurulum dosyasını indirin
2. Kurulum dosyasını çalıştırın
3. Kurulum sihirbazını takip edin

### Yöntem 2: Kaynak Koddan Derleme

```bash
# Repository'yi klonlayın
git clone https://github.com/harunkececi/passStore.git
cd passStore

# Projeyi derleyin
dotnet build PassStore.sln

# Uygulamayı çalıştırın
cd PassStore.WinForms
dotnet run
```

## 📖 Kullanım

1. **İlk Kullanım**: Uygulamayı açın ve yeni bir kullanıcı oluşturun
2. **Ana Şifre**: İlk girişte bir ana şifre (master password) belirleyin
3. **Şifre Ekleme**: Ana ekranda "Ekle" butonuna tıklayarak yeni şifre ekleyin
4. **Şifre Yönetimi**: Şifreleri görüntüleyin, düzenleyin veya silin
5. **Arama**: Üst kısımdaki arama kutusunu kullanarak şifrelerinizi arayın

## 🔐 Güvenlik

- Tüm şifreler AES-256 şifreleme ile korunur
- Kullanıcı şifreleri BCrypt ile hashlenir
- Ana şifre (master password) hiçbir zaman saklanmaz
- Veritabanı yerel olarak saklanır

## 📝 Lisans

© 2026 PassStore. Tüm hakları saklıdır.

## 👨‍💻 Geliştirici

**Harun KEÇECİ**
- 📧 E-posta: kececi.harun@gmail.com
- 🔗 GitHub: [@harunkececi](https://github.com/harunkececi)

## 🤝 Katkıda Bulunma

Katkılarınızı bekliyoruz! Lütfen önce bir Issue açın veya Pull Request gönderin.

## 📄 Changelog

### v1.0.0
- İlk sürüm
- Temel şifre yönetimi özellikleri
- Kullanıcı kayıt ve giriş sistemi
- AES şifreleme desteği
- Modern arayüz tasarımı
