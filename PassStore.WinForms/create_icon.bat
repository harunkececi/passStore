@echo off
echo Icon olusturuluyor...
echo.

REM Python'un yuklu olup olmadigini kontrol et
python --version >nul 2>&1
if errorlevel 1 (
    echo HATA: Python bulunamadi!
    echo Lutfen Python'u yukleyin: https://www.python.org/downloads/
    pause
    exit /b 1
)

REM Pillow kutuphanesini yukle
echo Pillow kutuphanesi kontrol ediliyor...
python -c "import PIL" >nul 2>&1
if errorlevel 1 (
    echo Pillow kutuphanesi yukleniyor...
    pip install Pillow
    if errorlevel 1 (
        echo HATA: Pillow yuklenemedi!
        pause
        exit /b 1
    )
)

REM Icon olustur
echo Icon olusturuluyor...
python create_icon.py

if errorlevel 1 (
    echo.
    echo HATA: Icon olusturulamadi!
    pause
    exit /b 1
)

echo.
echo Icon basariyla olusturuldu!
echo Dosya: app.ico
echo.
pause
