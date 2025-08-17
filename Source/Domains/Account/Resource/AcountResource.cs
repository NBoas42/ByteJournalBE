//Database Types
using Npgsql;
using Dapper;
using DbExtensions;


public class AccountResource {

    private readonly NpgsqlConnection postgresClient;

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

        if (account.Email != null) {
            insertColumns.Add("email");
            insertValues.Add("@Email");
            parameters.Add("@Email", account.Email);
        }

        if (account.Password != null) {
            insertColumns.Add("password");
            insertValues.Add("@Password");
            parameters.Add("@Password", account.Password);
        }

        if (account.Picture != null) {
            insertColumns.Add("picture");
            insertValues.Add("@Picture");
            parameters.Add("@Picture", account.Picture);
        }

        // Default field
        insertColumns.Add("permission_type");
        insertValues.Add("@PermissionType");
        parameters.Add("@PermissionType", "USER");

        // Use SqlBuilder here to assemble the full statement
        SqlBuilder sqlBuilder = new SqlBuilder();
        sqlBuilder.Append("INSERT INTO account");
        sqlBuilder.Append($"({string.Join(", ", insertColumns)})");
        sqlBuilder.Append("VALUES");
        sqlBuilder.Append($"({string.Join(", ", insertValues)})");
        sqlBuilder.Append("RETURNING id, name, email, picture AS \"Picture\", created_at AS \"CreatedAt\", updated_at AS \"UpdatedAt\"");

        string sql = sqlBuilder.ToString();

        Account? result = await this.postgresClient.QuerySingleAsync<Account>(sql, parameters);

        if (result != null) {
            return result;
        }

        throw new HttpException("Account Resource Error", 500);
    }

    public async Task<Account> UpdateAccount(Guid accountId, AccountUpdateDTO account) {
        bool hasSetClause = false;
        SqlBuilder sqlBuilder = new SqlBuilder();
        DynamicParameters parameters = new DynamicParameters();

        sqlBuilder.Append("UPDATE account SET ");

        if (account.Name != null) {
            sqlBuilder.Append(hasSetClause ? ", name = @Name " : "name = @Name ");
            parameters.Add("@Name", account.Name);
            hasSetClause = true;
        }

        if (account.Email != null) {
            sqlBuilder.Append(hasSetClause ? ", email = @Email " : "email = @Email ");
            parameters.Add("@Email", account.Email);
            hasSetClause = true;
        }

        if (account.Picture != null) {
            sqlBuilder.Append(hasSetClause ? ", picture = @Picture " : "picture = @Picture ");
            parameters.Add("@Picture", account.Picture);
            hasSetClause = true;
        }

        if (!hasSetClause) {
            throw new HttpException("No fields provided to update", 400);
        }

        sqlBuilder.Append("WHERE id = @Id ");
        parameters.Add("@Id", accountId);

        sqlBuilder.Append("RETURNING * ");

        string sql = sqlBuilder.ToString();

        Console.WriteLine(sql); // Debug
        Console.WriteLine(accountId);

        Account result = await this.postgresClient.QuerySingleAsync<Account>(sql, parameters);

        if (result != null) {
            return result;
        }

        throw new HttpException("Account Resource Error", 500);
    }

    async public Task<Account[]> SearchAccounts(AccountSearchDTO accountQuery) {
        bool hasSetClause = false;
        SqlBuilder sqlBuilder = new SqlBuilder();
        DynamicParameters parameters = new DynamicParameters();

        sqlBuilder.Append("SELECT id,name,email,picture,created_at AS createdAt,updated_at AS updatedAt FROM account WHERE ");

          if (accountQuery.Name != null) {
            sqlBuilder.Append(hasSetClause ? ", name = @Name " : "name = @Name ");
            parameters.Add("@Name", accountQuery.Name);
            hasSetClause = true;
        }

        if (accountQuery.Email != null) {
            sqlBuilder.Append(hasSetClause ? ", email = @Email " : "email = @Email ");
            parameters.Add("@Email", accountQuery.Email);
            hasSetClause = true;
        }

        if (parameters.ParameterNames.Count() < 1) {
            throw new HttpException("No Parameters included in query",400);
        }

        string sql = sqlBuilder.ToString();
        return (await this.postgresClient.QueryAsync<Account>(sql,parameters)).ToArray();
    }

    async public Task<Account> GetAccountByID(Guid accountId) {
        DynamicParameters parameters = new DynamicParameters();
        string sql = "SELECT id,name,email,picture,created_at AS createdAt,updated_at AS updatedAt FROM account WHERE account.id = @Id";
        Console.WriteLine(accountId);

        parameters.Add("@Id", accountId);
        return await this.postgresClient.QuerySingleAsync<Account>(sql, parameters);
    }

    async public Task<Account> UpdatePassword(Guid accountId, string password) {
        DynamicParameters parameters = new DynamicParameters();
        string sql = "UPDATE account SET password = @Password WHERE id = @Id";
        Console.WriteLine(accountId);

        parameters.Add("@Password", password);
        parameters.Add("@Id", accountId);
        return await this.postgresClient.QuerySingleAsync<Account>(sql, parameters);
    }

    async public Task<Account> DeleteAccountById(Guid accountId) {
        DynamicParameters parameters = new DynamicParameters();
        string sql = "DELETE FROM account WHERE account.id = @Id";

        parameters.Add("@Id", accountId);
        return await this.postgresClient.QuerySingleAsync<Account>(sql, parameters);
    }

}