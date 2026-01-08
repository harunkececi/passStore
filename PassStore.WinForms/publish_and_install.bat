@echo off
echo PassStore Self-Contained Kurulum Dosyasi Olusturuluyor...
echo.

REM Self-contained publish (tum bagimliliklari icerir)
echo Self-contained modda publish ediliyor...
dotnet publish PassStore.WinForms\PassStore.WinForms.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o "publish\win-x64"

if errorlevel 1 (
    echo HATA: Publish basarisiz!
    pause
    exit /b 1
)

echo.
echo Publish basarili!
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
    echo 3. Veya installer.iss dosyasini Inno Setup Compiler ile acip derleyin
    echo.
    pause
    exit /b 1
)

REM Installer klasorunu olustur
if not exist "Installer" mkdir "Installer"

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
echo Dosya konumu: Installer\PassStore_Setup_1.0.0.exe
echo.
echo NOT: Bu kurulum dosyasi .NET Runtime gerektirmez (self-contained)
echo.
pause
