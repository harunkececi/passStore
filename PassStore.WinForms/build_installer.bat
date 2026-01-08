@echo off
echo PassStore Kurulum Dosyasi Olusturuluyor...
echo.

REM Release modunda derle
echo Release modunda derleniyor...
dotnet build PassStore.sln -c Release
if errorlevel 1 (
    echo HATA: Derleme basarisiz!
    pause
    exit /b 1
)

echo.
echo Derleme basarili!
echo.

REM Inno Setup'un yuklu olup olmadigini kontrol et
where iscc >nul 2>&1
if errorlevel 1 (
    echo.
    echo UYARI: Inno Setup bulunamadi!
    echo.
    echo Inno Setup'u yuklemek icin:
    echo 1. https://jrsoftware.org/isinfo.php adresinden Inno Setup'u indirin
    echo 2. Yukleyin ve PATH'e ekleyin
    echo 3. Veya asagidaki komutu manuel olarak calistirin:
    echo    "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer.iss
    echo.
    echo Alternatif olarak, installer.iss dosyasini Inno Setup Compiler ile acip derleyebilirsiniz.
    echo.
    pause
    exit /b 1
)

REM Installer klasorunu olustur
if not exist "..\Installer" mkdir "..\Installer"

REM Inno Setup ile kurulum dosyasi olustur
echo Inno Setup ile kurulum dosyasi olusturuluyor...
iscc installer.iss

if errorlevel 1 (
    echo.
    echo HATA: Kurulum dosyasi olusturulamadi!
    pause
    exit /b 1
)

echo.
echo ========================================
echo Kurulum dosyasi basariyla olusturuldu!
echo ========================================
echo.
echo Dosya konumu: ..\Installer\PassStore_Setup_1.0.0.exe
echo.
pause
