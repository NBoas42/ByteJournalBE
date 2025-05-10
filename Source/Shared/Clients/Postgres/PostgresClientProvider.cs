using Microsoft.Data.SqlClient;
public class PostgresClientProvider {
    private  SqlConnection postgresClient;
    PostgresClientProvider(AppConfig config){
        string databaseURL = config.Get("databaseURL");
        string port = config.Get("port");
        string databaseName = config.Get("databaseName");
        string userId = config.Get("userId");
        string password =config.Get("password");
        this.postgresClient = new SqlConnection($"Server={databaseURL};Database={databaseName};User Id={userId};Password={password};");
    }

    public SqlConnection getPostgresClient () {
        return this.postgresClient;
    }
    

}