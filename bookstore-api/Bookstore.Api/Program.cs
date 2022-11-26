using Bookstore.Api.Extensions;
using Bookstore.Api.Middleware;
using Bookstore.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices(builder.Configuration);

var origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
var policyName = "bookstoreUI";

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, builder =>
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(origins)
        .AllowCredentials()
    );
});

var app = builder.Build();

app.UseCors(policyName);

app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
