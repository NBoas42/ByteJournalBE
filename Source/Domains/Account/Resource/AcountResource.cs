//Database Types
using Npgsql;
using Dapper;
using DbExtensions.SqlBuilder
public class AccountResource {

    NpgsqlConnection postgresClient;
    public AccountResource(PostgresClientProvider postgresClientProvider) {
        this.postgresClient = postgresClientProvider.getPostgresClient();
    }

    async public Task<Account> CreateAccount(Account account) {
        List<string> insertColumns = new List<string>();
        List<string> insertValues = new List<string>();
        DynamicParameters parameters = new DynamicParameters();

        if (account.Name != null) {
            insertColumns.Add("name");
            insertValues.Add("@Name");
            parameters.Add("@Name", account.Name);
        }

        if (account.Email != null){
            insertColumns.Add("email");
            insertValues.Add("@Email");
            parameters.Add("@Email", account.Email);
        }

        if (account.Password != null){
            insertColumns.Add("password");
            insertValues.Add("@Password");
            parameters.Add("@Password", account.Password);
        }

        if (account.Picture != null){
            insertColumns.Add("picture");
            insertValues.Add("@Picture");
            parameters.Add("@Picture", account.Picture);
        }

        // Add Default Permission Type
        insertColumns.Add("permission_type");
        insertValues.Add("@permission_type");
        parameters.Add("@permission_type", "USER");

        string sql = $"""
        INSERT INTO account ({string.Join(", ", insertColumns)}) 
        VALUES ({string.Join(", ", insertValues)}) 
        RETURNING id, name, email, picture AS \"Picture\", created_at AS \"CreatedAt\", updated_at AS \"UpdatedAt\"
        """;

        Account? result = await this.postgresClient.QuerySingleAsync<Account>(sql, parameters);

        if (result != null) {
            return result;
        }

        throw new HttpException("Account Resource Error", 500);
    }

async public Task<Account> UpdateAccount(Guid accountId, AccountUpdateDTO account) {
    SqlBuilder sqlBuilder = new SqlBuilder();
    DynamicParameters parameters = new DynamicParameters();

    parameters.Add("@Id", accountId);

    Template template = sqlBuilder.AddTemplate("UPDATE account SET /**set**/ WHERE id = @Id RETURNING *");

    if (account.Name != null) {
        sqlBuilder.Set("name = @Name");
        parameters.Add("@Name", account.Name);
    }

    if (account.Email != null) {
        sqlBuilder.Set("email = @Email");
        parameters.Add("@Email", account.Email);
    }

    if (account.Picture != null) {
        sqlBuilder.Set("picture = @Picture");
        parameters.Add("@Picture", account.Picture);
    }

    if (!template.RawSql.Contains("SET")) {
        throw new HttpException("No fields provided to update", 400);
    }

    Account? result = await this.postgresClient.QuerySingleAsync<Account>(template.RawSql, parameters);

    if (result != null) {
        return result;
    }

    throw new HttpException("Account Resource Error", 500);
}

    async public Task<Account[]> SearchAccounts(Account AccountQuery) {
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