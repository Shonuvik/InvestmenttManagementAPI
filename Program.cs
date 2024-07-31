using System.Text;
using System.Text.Json.Serialization;
using InvestmentManagement.Application;
using InvestmentManagement.Application.Interfaces;
using InvestmentManagement.Helpers.Extensions;
using InvestmentManagement.Infrastructure.Repositories.Commands;
using InvestmentManagement.Infrastructure.Repositories.Interfaces;
using InvestmentManagement.Infrastructure.Repositories.Querys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader();
        });
});

builder.Services.AddDatabase(configuration);
AddIoC(builder.Services);

builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Investment Management API"));

app.Run();


static void AddIoC(IServiceCollection services)
{
    //Repositories
    services.AddScoped<IAssetQueryRepository, AssetQueryRepository>();
    services.AddScoped<ITransactionQueryRepository, TransactionQueryRepository>();
    services.AddScoped<IUserQueryRepository, UserQueryRepository>();
    services.AddScoped<IPortfolioQueryRepository, PortfolioQueryRepository>();

    //Commands
    services.AddScoped<IPortfolioCommandRepository, PortfolioCommandRepository>();
    services.AddScoped<ITransactionCommandRepository, TransactionCommandRepository>();

    services.AddScoped<IAssetSellHandler, AssetSellHandler>();
    services.AddScoped<IAssetPurchaseHandler, AssetPurchaseHandler>();
    //Handler
}
