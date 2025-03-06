var builder = WebApplication.CreateBuilder(args);

// Dodavanje DbContext-a sa konekcijskim stringom
builder.Services.AddDbContext<BankaContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Konfiguracija CORS-a
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:4209", "https://akulbank.netlify.app", "https://akulbank.onrender.com");
    });
});

builder.WebHost.UseUrls("http://*:8080"); // Postavljanje porta

// Dodavanje kontrolera
builder.Services.AddControllers();

// Registracija servisa za autentifikaciju
builder.Services.AddScoped<AuthService>();

// Swagger konfiguracija
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Korišćenje Swagger-a u development i production okruženju
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(app.Environment.IsDevelopment() ? "/swagger/v1/swagger.json" : "https://akulbank.onrender.com/swagger/v1/swagger.json", "Banka API v1");
        options.RoutePrefix = "api/docs";
    });
}

// Korišćenje statičkih fajlova
app.UseDefaultFiles();
app.UseStaticFiles();

// Aktiviranje CORS-a
app.UseCors("CORS");

// Aktiviranje autorizacije
app.UseAuthorization();

// Mapiranje kontrolera
app.MapControllers();

// Default endpoint za testiranje
app.MapGet("/api/welcome", () =>
{
    return Results.Ok("Welcome! The application is running.");
});

// Pokretanje aplikacije
app.Run();
