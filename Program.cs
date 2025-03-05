var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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

builder.WebHost.UseUrls("http://*:8080");

builder.Services.AddControllers();

builder.Services.AddScoped<AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("https://akulbank.onrender.com/swagger/v1/swagger.json", "Banka API v1");
        options.RoutePrefix = "api/docs";
    });
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/welcome", () =>
{
    return Results.Ok("Welcome! The application is running.");
});

app.Run();
