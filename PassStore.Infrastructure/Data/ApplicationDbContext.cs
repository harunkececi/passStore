using Microsoft.EntityFrameworkCore;
using PassStore.Core.Entities;

namespace PassStore.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Password> Passwords { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Password>(entity =>
        {
            entity.ToTable("passwords");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
            entity.Property(e => e.Baslik).HasColumnName("baslik").IsRequired();
            entity.Property(e => e.KullaniciAdi).HasColumnName("kullanici_adi").IsRequired();
            entity.Property(e => e.SifrelenmisSifre).HasColumnName("sifrelenmis_sifre").IsRequired();
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.Notlar).HasColumnName("notlar");
            entity.Property(e => e.Kategori).HasColumnName("kategori");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.OlusturanKullanici).HasColumnName("olusturan_kullanici");
            entity.Property(e => e.OlusturmaTarihi).HasColumnName("olusturma_tarihi");
            entity.Property(e => e.GuncelleyenKullanici).HasColumnName("guncelleyen_kullanici");
            entity.Property(e => e.GuncellemeTarihi).HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.SilenKullanici).HasColumnName("silen_kullanici");
            entity.Property(e => e.SilinmeTarihi).HasColumnName("silinme_tarihi");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
            entity.Property(e => e.KullaniciAdi).HasColumnName("kullanici_adi").IsRequired();
            entity.Property(e => e.SifrelenmisSifre).HasColumnName("sifrelenmis_sifre").IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired();
            entity.Property(e => e.AktifMi).HasColumnName("aktif_mi");
            entity.Property(e => e.OlusturanKullanici).HasColumnName("olusturan_kullanici");
            entity.Property(e => e.OlusturmaTarihi).HasColumnName("olusturma_tarihi");
            entity.Property(e => e.GuncelleyenKullanici).HasColumnName("guncelleyen_kullanici");
            entity.Property(e => e.GuncellemeTarihi).HasColumnName("guncelleme_tarihi");
            entity.Property(e => e.SilenKullanici).HasColumnName("silen_kullanici");
            entity.Property(e => e.SilinmeTarihi).HasColumnName("silinme_tarihi");
        });
    }
}
