namespace PassStore.Core.Entities;

public class Password : BaseEntity
{
    public string Baslik { get; set; } = string.Empty;
    public string KullaniciAdi { get; set; } = string.Empty;
    public string SifrelenmisSifre { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string? Notlar { get; set; }
    public string? Kategori { get; set; }
    public string KullaniciId { get; set; } = string.Empty;
}
