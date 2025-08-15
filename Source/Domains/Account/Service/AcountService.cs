using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class AccountService {

    private readonly AccountResource accountResource;

    public AccountService(AccountResource accountResource) {
        this.accountResource = accountResource;
    }

    private string HashPassword(string password) {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
    }

    private bool VerifyPassword(string password, string hashedPassword) {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }

    public async Task<bool> Authenticate(string password, string email) {
        AccountSearchDTO emailSearch = new AccountSearchDTO();
        emailSearch.Email = email;
        Account[] accounts = await this.accountResource.SearchAccounts(emailSearch);
        if (accounts.Length < 1) {
            throw new HttpException("Invalid Password Or Email", 403);
        }
        Account accountToAuthenticate = accounts[0];
        return this.VerifyPassword(password, accountToAuthenticate.Password);
    }

    public async Task<Account> UpdatePassword(string oldPassword, string newPassword, Guid accountId) {
        Account accountToUpdate = await this.accountResource.GetAccountByID(accountId);
        bool validOldPassword =  this.VerifyPassword(oldPassword, accountToUpdate.Password);
        if (!validOldPassword) {
            throw new HttpException("Old Password Does Not Match", 403);
        }
        Account updatedAccount = await this.accountResource.UpdatePassword(accountId, newPassword);
        return updatedAccount;
    }

    async public Task<Account> Create(Account account) {
        account.Password = this.HashPassword(account.Password);
        Account result = await this.accountResource.CreateAccount(account);
        return result;
    }

    async public Task<Account> Update(Guid accountId, AccountUpdateDTO accountToUpdate) {
        Account result = await this.accountResource.UpdateAccount(accountId, accountToUpdate);
        return result;
    }

    async public Task<Account[]> Search(AccountSearchDTO accountQuery) {
        Account[] result = await this.accountResource.SearchAccounts(accountQuery);
        return result;
    }

    async public Task<Account> GetByID(Guid accountId) {
        Account result = await this.accountResource.GetAccountByID(accountId);
        return result;
    }

    async public Task<Account> DeleteById(Guid accountId) {
        Account result = await this.accountResource.DeleteAccountById(accountId);
        return result;
    }

}