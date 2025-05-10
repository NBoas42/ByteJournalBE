//Database Types
using Microsoft.Data.SqlClient;
using Dapper;
public class AccountResource {
    
    SqlConnection postgresClient;
    AccountResource(PostgresClientProvider postgresClientProvider){
        this.postgresClient = postgresClientProvider.getPostgresClient();
    }

   async public Task<Account[]> searchAccounts(){
        Account[] result = (await this.postgresClient.QueryAsync<Account>("SELECT * FROM account")).ToArray();
        return result;
    }

}