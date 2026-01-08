namespace PassStore.Core.Entities;

public class User : BaseEntity
{
    public string KullaniciAdi { get; set; } = string.Empty;
    public string SifrelenmisSifre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool AktifMi { get; set; } = true;
}
