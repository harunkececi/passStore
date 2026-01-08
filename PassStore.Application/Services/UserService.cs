using PassStore.Core.Entities;
using PassStore.Core.Interfaces;

namespace PassStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> LoginAsync(string kullaniciAdi, string sifre)
    {
        var users = await _unitOfWork.UserRepository.FindAsync(u => 
            u.KullaniciAdi == kullaniciAdi && u.AktifMi);
        
        var user = users.FirstOrDefault();
        if (user == null)
            return null;

        if (ValidatePasswordAsync(sifre, user.SifrelenmisSifre).Result)
            return user;

        return null;
    }

    public async Task<User> RegisterAsync(string kullaniciAdi, string sifre, string email)
    {
        var existingUsers = await _unitOfWork.UserRepository.FindAsync(u => 
            u.KullaniciAdi == kullaniciAdi || u.Email == email);
        
        if (existingUsers.Any())
            throw new Exception("Kullanıcı adı veya email zaten kullanılıyor");

        var user = new User
        {
            KullaniciAdi = kullaniciAdi,
            Email = email,
            SifrelenmisSifre = HashPassword(sifre),
            AktifMi = true
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ValidatePasswordAsync(string sifre, string hash)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.Verify(sifre, hash));
    }

    public string HashPassword(string sifre)
    {
        return BCrypt.Net.BCrypt.HashPassword(sifre);
    }

    public async Task<User?> GetUserByUsernameAsync(string kullaniciAdi)
    {
        var users = await _unitOfWork.UserRepository.FindAsync(u => 
            u.KullaniciAdi == kullaniciAdi && u.AktifMi);
        return users.FirstOrDefault();
    }
}
