namespace PassStore.Core.Entities;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? OlusturanKullanici { get; set; }
    public DateTime? OlusturmaTarihi { get; set; }
    public string? GuncelleyenKullanici { get; set; }
    public DateTime? GuncellemeTarihi { get; set; }
    public string? SilenKullanici { get; set; }
    public DateTime? SilinmeTarihi { get; set; }
    
    public bool SilindiMi => SilinmeTarihi.HasValue;
}
