var builder = WebApplication.CreateBuilder(args);

// Register Shared Classes
builder.Services.AddSingleton(new AppConfig("./source/local.config"));
builder.Services.AddSingleton<PostgresClientProvider>();

// Register Account Domain
builder.Services.AddTransient<AccountResource>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment()){
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
