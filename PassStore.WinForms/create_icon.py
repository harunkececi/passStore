"""
Icon Oluşturma Scripti
Bu script, şifre yönetimi uygulaması için basit bir icon oluşturur.
"""

try:
    from PIL import Image, ImageDraw, ImageFont
    import os
except ImportError:
    print("Pillow kütüphanesi bulunamadı. Lütfen şu komutu çalıştırın:")
    print("pip install Pillow")
    exit(1)

def create_icon():
    # Icon boyutları (Windows için standart boyutlar)
    sizes = [16, 32, 48, 64, 128, 256]
    images = []
    
    # Pillow versiyonunu kontrol et
    try:
        from PIL import __version__
        pillow_version = tuple(map(int, __version__.split('.')[:2]))
        has_rounded_rect = pillow_version >= (8, 0)
    except:
        has_rounded_rect = False
    
    for size in sizes:
        # Yeni bir görüntü oluştur (RGBA - şeffaf arka plan için)
        img = Image.new('RGBA', (size, size), (0, 0, 0, 0))
        draw = ImageDraw.Draw(img)
        
        # Arka plan - mavi tonlarında yuvarlak köşeli kare
        bg_color = (74, 144, 226, 255)  # Mavi
        padding = size // 8
        
        if has_rounded_rect:
            draw.rounded_rectangle(
                [padding, padding, size - padding, size - padding],
                radius=size // 6,
                fill=bg_color
            )
        else:
            # Eski versiyonlar için normal dikdörtgen
            draw.rectangle(
                [padding, padding, size - padding, size - padding],
                fill=bg_color
            )
        
        # Kilit simgesi çizimi
        lock_color = (255, 255, 255, 255)  # Beyaz
        line_width = max(2, size // 16)
        
        # Kilit gövdesi boyutları
        lock_body_height = size // 2
        lock_body_width = size // 2.5
        lock_x = int((size - lock_body_width) / 2)
        lock_y = int(size // 2.5)
        
        # Kilit kemeri (üst yarım daire)
        arc_radius = lock_body_width / 2
        arc_top = int(lock_y - arc_radius)
        draw.arc(
            [lock_x, arc_top, int(lock_x + lock_body_width), int(arc_top + lock_body_width)],
            start=180,
            end=0,
            fill=lock_color,
            width=line_width
        )
        
        # Kilit gövdesi
        if has_rounded_rect:
            draw.rounded_rectangle(
                [lock_x, lock_y, int(lock_x + lock_body_width), int(lock_y + lock_body_height)],
                radius=size // 20,
                outline=lock_color,
                width=line_width
            )
        else:
            draw.rectangle(
                [lock_x, lock_y, int(lock_x + lock_body_width), int(lock_y + lock_body_height)],
                outline=lock_color,
                width=line_width
            )
        
        # Kilit deliği (küçük daire)
        keyhole_size = size // 8
        keyhole_x = size / 2
        keyhole_y = lock_y + lock_body_height / 2
        draw.ellipse(
            [int(keyhole_x - keyhole_size/2), int(keyhole_y - keyhole_size/2),
             int(keyhole_x + keyhole_size/2), int(keyhole_y + keyhole_size/2)],
            fill=lock_color
        )
        
        images.append(img)
    
    # ICO dosyası olarak kaydet
    output_path = os.path.join(os.path.dirname(__file__), 'app.ico')
    
    # Tüm boyutları ICO dosyasına kaydet
    # ICO formatı için RGB formatına çevir (RGBA'dan)
    rgb_images = []
    for img in images:
        # RGBA'dan RGB'ye çevir (beyaz arka plan ile)
        rgb_img = Image.new('RGB', img.size, (255, 255, 255))
        rgb_img.paste(img, mask=img.split()[3] if img.mode == 'RGBA' else None)
        rgb_images.append(rgb_img)
    
    try:
        # En büyük görüntüyü kullan ve tüm boyutları belirt
        rgb_images[-1].save(
            output_path,
            format='ICO',
            sizes=[(img.size[0], img.size[1]) for img in rgb_images]
        )
    except Exception as e:
        # Eğer çoklu boyut kaydedilemezse, en büyük boyutu kaydet
        print(f"Uyari: Tum boyutlar kaydedilemedi, sadece 256x256 kaydediliyor. ({e})")
        rgb_images[-1].save(output_path, format='ICO')
    
    print(f"Icon basariyla olusturuldu: {output_path}")
    print(f"  Boyutlar: {', '.join([str(s) + 'x' + str(s) for s in sizes])}")

if __name__ == '__main__':
    create_icon()
