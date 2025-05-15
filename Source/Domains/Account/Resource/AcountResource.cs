//Database Types
using Npgsql;
using Dapper;
public class AccountResource {
    
    NpgsqlConnection postgresClient;
    public AccountResource(PostgresClientProvider postgresClientProvider){
        this.postgresClient = postgresClientProvider.getPostgresClient();
    }

   async public Task<Account[]> searchAccounts(){
        Account[] result = (await this.postgresClient.QueryAsync<Account>("SELECT * FROM account")).ToArray();
        return result;
    }

}