using PassStore.Core.Entities;

namespace PassStore.Core.Interfaces;

public interface IUserService
{
    Task<User?> LoginAsync(string kullaniciAdi, string sifre);
    Task<User> RegisterAsync(string kullaniciAdi, string sifre, string email);
    Task<User?> GetUserByUsernameAsync(string kullaniciAdi);
    Task<bool> ValidatePasswordAsync(string sifre, string hash);
    string HashPassword(string sifre);
}
