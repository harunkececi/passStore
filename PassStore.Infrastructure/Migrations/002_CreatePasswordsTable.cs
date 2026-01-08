using FluentMigrator;

namespace PassStore.Infrastructure.Migrations;

[Migration(20240101000002)]
public class CreatePasswordsTable : Migration
{
    public override void Up()
    {
        Create.Table("passwords")
            .WithColumn("id").AsString(36).PrimaryKey()
            .WithColumn("baslik").AsString(200).NotNullable()
            .WithColumn("kullanici_adi").AsString(200).NotNullable()
            .WithColumn("sifrelenmis_sifre").AsString(1000).NotNullable()
            .WithColumn("url").AsString(500).Nullable()
            .WithColumn("notlar").AsString(2000).Nullable()
            .WithColumn("kategori").AsString(100).Nullable()
            .WithColumn("kullanici_id").AsString(36).NotNullable()
            .WithColumn("olusturan_kullanici").AsString(36).Nullable()
            .WithColumn("olusturma_tarihi").AsDateTime().Nullable()
            .WithColumn("guncelleyen_kullanici").AsString(36).Nullable()
            .WithColumn("guncelleme_tarihi").AsDateTime().Nullable()
            .WithColumn("silen_kullanici").AsString(36).Nullable()
            .WithColumn("silinme_tarihi").AsDateTime().Nullable();

        // SQLite foreign key desteği sınırlı olduğu için foreign key constraint'i kaldırıldı
        // İlişki uygulama katmanında kontrol edilecek
    }

    public override void Down()
    {
        Delete.Table("passwords");
    }
}
