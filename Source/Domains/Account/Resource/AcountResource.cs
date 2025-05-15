//Database Types
using Npgsql;
using Dapper;
public class AccountResource {

    NpgsqlConnection postgresClient;
    public AccountResource(PostgresClientProvider postgresClientProvider) {
        this.postgresClient = postgresClientProvider.getPostgresClient();
    }

    async public Task<Account> CreateAccount(Account account) {
        Account[] result = (await this.postgresClient.QueryAsync<Account>("SELECT * FROM account")).ToArray();
        return result[0];
    }

    async public Task<Account> UpdateAccount(Account account) {
        Account[] result = (await this.postgresClient.QueryAsync<Account>("SELECT * FROM account")).ToArray();
        return result[0];
    }

    async public Task<Account[]> SearchAccounts(Account accountQuery) {
        Account[] result = (await this.postgresClient.QueryAsync<Account>("SELECT * FROM account")).ToArray();
        return result;
    }

    async public Task<Account> GetAccountByID(Guid accountId) {
        Account[] result = (await this.postgresClient.QueryAsync<Account>($"SELECT * FROM account WHERE account.id = {accountId}")).ToArray();
        return result[0];
    }
    
    async public Task<Account> DeleteAccountById(Guid accountId) {
        Account[] result = (await this.postgresClient.QueryAsync<Account>($"DELETE FROM account WHERE account.id = {accountId}")).ToArray();
        return result[0];
    }

}