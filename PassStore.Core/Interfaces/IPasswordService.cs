using PassStore.Core.Entities;

namespace PassStore.Core.Interfaces;

public interface IPasswordService
{
    Task<Password> AddPasswordAsync(Password password, string masterPassword);
    Task<Password> UpdatePasswordAsync(Password password, string masterPassword);
    Task DeletePasswordAsync(Password password);
    Task<IEnumerable<Password>> GetPasswordsByUserIdAsync(string userId);
    Task<Password?> GetPasswordByIdAsync(string id);
    Task<string> DecryptPasswordAsync(string encryptedPassword, string masterPassword);
    string EncryptPassword(string plainPassword, string masterPassword);
}
