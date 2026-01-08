# PassStore Kurulum Dosyası Oluşturma Scripti

Write-Host "PassStore Kurulum Dosyasi Olusturuluyor..." -ForegroundColor Cyan
Write-Host ""

# Release modunda derle
Write-Host "Release modunda derleniyor..." -ForegroundColor Yellow
dotnet build PassStore.sln -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "HATA: Derleme basarisiz!" -ForegroundColor Red
    Read-Host "Devam etmek icin Enter'a basin"
    exit 1
}

Write-Host ""
Write-Host "Derleme basarili!" -ForegroundColor Green
Write-Host ""

# Inno Setup'un yüklü olup olmadığını kontrol et
$innoSetupPath = Get-Command iscc -ErrorAction SilentlyContinue

if (-not $innoSetupPath) {
    # Alternatif konumları kontrol et
    $possiblePaths = @(
        "C:\Program Files (x86)\Inno Setup 6\ISCC.exe",
        "C:\Program Files\Inno Setup 6\ISCC.exe",
        "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
        "$env:ProgramFiles\Inno Setup 6\ISCC.exe"
    )
    
    $innoSetupExe = $null
    foreach ($path in $possiblePaths) {
        if (Test-Path $path) {
            $innoSetupExe = $path
            break
        }
    }
    
    if (-not $innoSetupExe) {
        Write-Host ""
        Write-Host "UYARI: Inno Setup bulunamadi!" -ForegroundColor Yellow
        Write-Host ""
        Write-Host "Inno Setup'u yuklemek icin:" -ForegroundColor Cyan
        Write-Host "1. https://jrsoftware.org/isinfo.php adresinden Inno Setup'u indirin"
        Write-Host "2. Yukleyin ve PATH'e ekleyin"
        Write-Host "3. Veya asagidaki komutu manuel olarak calistirin:" -ForegroundColor Yellow
        Write-Host '   "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer.iss' -ForegroundColor Gray
        Write-Host ""
        Write-Host "Alternatif olarak, installer.iss dosyasini Inno Setup Compiler ile acip derleyebilirsiniz." -ForegroundColor Cyan
        Write-Host ""
        Read-Host "Devam etmek icin Enter'a basin"
        exit 1
    }
} else {
    $innoSetupExe = "iscc"
}

# Installer klasörünü oluştur
$installerDir = Join-Path (Split-Path $PSScriptRoot -Parent) "Installer"
if (-not (Test-Path $installerDir)) {
    New-Item -ItemType Directory -Path $installerDir | Out-Null
}

# Inno Setup ile kurulum dosyası oluştur
Write-Host "Inno Setup ile kurulum dosyasi olusturuluyor..." -ForegroundColor Yellow
& $innoSetupExe installer.iss

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "HATA: Kurulum dosyasi olusturulamadi!" -ForegroundColor Red
    Read-Host "Devam etmek icin Enter'a basin"
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "Kurulum dosyasi basariyla olusturuldu!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "Dosya konumu: $installerDir\PassStore_Setup_1.0.0.exe" -ForegroundColor Cyan
Write-Host ""
Read-Host "Devam etmek icin Enter'a basin"
