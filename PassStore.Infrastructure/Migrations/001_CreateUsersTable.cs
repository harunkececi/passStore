using FluentMigrator;

namespace PassStore.Infrastructure.Migrations;

[Migration(20240101000001)]
public class CreateUsersTable : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsString(36).PrimaryKey()
            .WithColumn("kullanici_adi").AsString(100).NotNullable().Unique()
            .WithColumn("sifrelenmis_sifre").AsString(500).NotNullable()
            .WithColumn("email").AsString(200).NotNullable()
            .WithColumn("aktif_mi").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("olusturan_kullanici").AsString(36).Nullable()
            .WithColumn("olusturma_tarihi").AsDateTime().Nullable()
            .WithColumn("guncelleyen_kullanici").AsString(36).Nullable()
            .WithColumn("guncelleme_tarihi").AsDateTime().Nullable()
            .WithColumn("silen_kullanici").AsString(36).Nullable()
            .WithColumn("silinme_tarihi").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}
