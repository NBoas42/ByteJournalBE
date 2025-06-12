using Npgsql;

// Could possible replace this iwht a more basic library?
public class PostgresClientProvider {
    private NpgsqlConnection postgresClient;
    public PostgresClientProvider(AppConfig config) {

        string databaseURL = config.Get("databaseURL");
        string port = config.Get("port");
        string databaseName = config.Get("databaseName");
        string userId = config.Get("userId");
        string password = config.Get("password");
        string connString = $"Host={databaseURL};Port={port};Database={databaseName};Username={userId};Password={password};";

        Console.WriteLine($"Connecting to Postgres at {connString}");

        this.postgresClient = new NpgsqlConnection(connString);
    }

    public NpgsqlConnection getPostgresClient() {
        return this.postgresClient;
    }


}