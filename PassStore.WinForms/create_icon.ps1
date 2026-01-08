# Icon Oluşturma PowerShell Scripti

Write-Host "Icon olusturuluyor..." -ForegroundColor Cyan
Write-Host ""

# Python'un yüklü olup olmadığını kontrol et
try {
    $pythonVersion = python --version 2>&1
    Write-Host "Python bulundu: $pythonVersion" -ForegroundColor Green
} catch {
    Write-Host "HATA: Python bulunamadi!" -ForegroundColor Red
    Write-Host "Lutfen Python'u yukleyin: https://www.python.org/downloads/" -ForegroundColor Yellow
    Read-Host "Devam etmek icin Enter'a basin"
    exit 1
}

# Pillow kütüphanesini kontrol et ve yükle
Write-Host "Pillow kutuphanesi kontrol ediliyor..." -ForegroundColor Cyan
try {
    python -c "import PIL" 2>&1 | Out-Null
    Write-Host "Pillow kutuphanesi mevcut." -ForegroundColor Green
} catch {
    Write-Host "Pillow kutuphanesi yukleniyor..." -ForegroundColor Yellow
    pip install Pillow
    if ($LASTEXITCODE -ne 0) {
        Write-Host "HATA: Pillow yuklenemedi!" -ForegroundColor Red
        Read-Host "Devam etmek icin Enter'a basin"
        exit 1
    }
}

# Icon oluştur
Write-Host "Icon olusturuluyor..." -ForegroundColor Cyan
python create_icon.py

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "Icon basariyla olusturuldu!" -ForegroundColor Green
    Write-Host "Dosya: app.ico" -ForegroundColor Cyan
} else {
    Write-Host ""
    Write-Host "HATA: Icon olusturulamadi!" -ForegroundColor Red
}

Write-Host ""
Read-Host "Devam etmek icin Enter'a basin"
