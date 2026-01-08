using PassStore.Core.Entities;
using PassStore.Core.Interfaces;
using PassStore.Infrastructure.Services;

namespace PassStore.Application.Services;

public class PasswordService : IPasswordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly EncryptionService _encryptionService;

    public PasswordService(IUnitOfWork unitOfWork, EncryptionService encryptionService)
    {
        _unitOfWork = unitOfWork;
        _encryptionService = encryptionService;
    }

    public async Task<Password> AddPasswordAsync(Password password, string masterPassword)
    {
        password.SifrelenmisSifre = _encryptionService.Encrypt(password.SifrelenmisSifre, masterPassword);
        await _unitOfWork.PasswordRepository.AddAsync(password);
        await _unitOfWork.SaveChangesAsync();
        return password;
    }

    public async Task<Password> UpdatePasswordAsync(Password password, string masterPassword)
    {
        var existingPassword = await _unitOfWork.PasswordRepository.GetByIdAsync(password.Id);
        if (existingPassword == null)
            throw new Exception("Şifre bulunamadı");

        existingPassword.Baslik = password.Baslik;
        existingPassword.KullaniciAdi = password.KullaniciAdi;
        existingPassword.Url = password.Url;
        existingPassword.Notlar = password.Notlar;
        existingPassword.Kategori = password.Kategori;
        existingPassword.SifrelenmisSifre = _encryptionService.Encrypt(password.SifrelenmisSifre, masterPassword);

        await _unitOfWork.PasswordRepository.UpdateAsync(existingPassword);
        await _unitOfWork.SaveChangesAsync();
        return existingPassword;
    }

    public async Task<string> DecryptPasswordAsync(string encryptedPassword, string masterPassword)
    {
        return _encryptionService.Decrypt(encryptedPassword, masterPassword);
    }

    public string EncryptPassword(string plainPassword, string masterPassword)
    {
        return _encryptionService.Encrypt(plainPassword, masterPassword);
    }

    public async Task DeletePasswordAsync(Password password)
    {
        await _unitOfWork.PasswordRepository.DeleteAsync(password);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Password>> GetPasswordsByUserIdAsync(string userId)
    {
        return await _unitOfWork.PasswordRepository.FindAsync(p => p.KullaniciId == userId);
    }

    public async Task<Password?> GetPasswordByIdAsync(string id)
    {
        return await _unitOfWork.PasswordRepository.GetByIdAsync(id);
    }
}
