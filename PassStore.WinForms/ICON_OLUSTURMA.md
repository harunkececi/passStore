# Icon Oluşturma Rehberi

Uygulamaya icon eklemek için `app.ico` dosyasını `PassStore.WinForms` klasörüne eklemeniz gerekiyor.

## Yöntem 1: Online Icon Generator Kullanma

1. Aşağıdaki sitelerden birini kullanarak icon oluşturun:
   - https://www.favicon-generator.org/
   - https://convertio.co/png-ico/
   - https://www.icoconverter.com/

2. Şifre/kilit temalı bir icon seçin veya oluşturun
3. Dosyayı `app.ico` olarak kaydedin
4. `PassStore.WinForms` klasörüne kopyalayın

## Yöntem 2: Mevcut Icon Kullanma

1. İnternetten şifre yönetimi/kilit temalı bir icon indirin
2. PNG veya JPG formatındaysa, yukarıdaki sitelerden birini kullanarak .ico formatına dönüştürün
3. Dosyayı `app.ico` olarak `PassStore.WinForms` klasörüne kopyalayın

## Yöntem 3: Visual Studio ile Icon Oluşturma

1. Visual Studio'da yeni bir proje oluşturun
2. Resources klasörüne sağ tıklayın > Add > New Item > Icon File
3. Icon'u düzenleyin
4. Oluşturulan .ico dosyasını `PassStore.WinForms` klasörüne kopyalayın ve `app.ico` olarak yeniden adlandırın

## Not

Icon dosyası eklendikten sonra projeyi yeniden derleyin. Icon tüm form'larda ve uygulama executable'ında görünecektir.
