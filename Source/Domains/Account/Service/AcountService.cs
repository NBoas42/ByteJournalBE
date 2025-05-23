//Database Types
using Npgsql;
using Dapper;
public class AccountService {

   private readonly AccountResource _AccountResource;

    public AccountService(AccountResource accountResource) {
        this._AccountResource = accountResource;
    }

    async public Task<Account> CreateAccount(Account account) {
        // Add Password Encryption here

        Account result = await this._AccountResource.CreateAccount(account);

        return result;
    }

    async public Task<Account> UpdateAccount(Account account) {
        Account result = await this._AccountResource.UpdateAccount(account);
        return result;
    }

    async public Task<Account[]> SearchAccounts(Account accountQuery) {
        Account[] result = await this._AccountResource.SearchAccounts(accountQuery);
        return result;
    }

    async public Task<Account> GetAccountByID(Guid accountId) {
        Account result = await this._AccountResource.GetAccountByID(accountId);
        return result;
    }
    
    async public Task<Account> DeleteAccountById(Guid accountId) {
        Account result = await this._AccountResource.DeleteAccountById(accountId);
        return result;
    }

}