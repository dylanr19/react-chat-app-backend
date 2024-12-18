using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using react_chat_app_backend.Context;
using react_chat_app_backend.Controllers.WSControllers;
using react_chat_app_backend.Repositories;
using react_chat_app_backend.Repositories.Interfaces;
using react_chat_app_backend.Services;
using react_chat_app_backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration["ConnectionString"];

// builder.Logging.AddAzureWebAppDiagnostics();
// builder.Services.Configure<AzureFileLoggerOptions>(options =>
// {
//     options.FileName = "logs-";
//     options.FileSizeLimit = 50 * 1024;
//     options.RetainedFileCountLimit = 5;
// });
//
// builder.Services.Configure<AzureBlobLoggerOptions>(options =>
// {
//     options.BlobName = "log.txt";
// });

builder.Services.AddDbContext<AppDbContext>(optionsBuilder => 
    optionsBuilder.UseSqlite("Data Source=Application.db"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<IWSManager, WSManager>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddScoped<IWSMessageRepository, WSMessageRepository>();
builder.Services.AddScoped<IWSMessageService, WSMessageService>();

builder.Services.AddScoped<IFriendShipRepository, FriendShipRepository>();
builder.Services.AddScoped<IFriendShipService, FriendShipService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// builder.Services.AddRateLimiter(options =>
// {
//     options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
//
//     options.AddFixedWindowLimiter("fixed", rateLimitOptions =>
//     {
//         rateLimitOptions.PermitLimit = 10;
//         rateLimitOptions.Window = TimeSpan.FromMinutes(5);
//         rateLimitOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//         rateLimitOptions.QueueLimit = 0;
//     });
// });

var allowedOrigins = builder.Configuration["AllowedOrigins"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //options.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy();
    //options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// app.UseRateLimiter();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebSockets();

app.UseAuthorization();

app.MapControllers();

app.Run();