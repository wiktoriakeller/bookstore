using Bookstore.WebApi.Extensions;
using Bookstore.WebApi.Middleware;

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
