using System.Text;
using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Middleware;
using Controle_Financeiro___Back.Models;
using Controle_Financeiro___Back.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var conectionString = builder.Configuration.GetConnectionString("ExpenseConnection");
builder.Services.AddDbContext<FinaceContext>(opts =>
opts.UseLazyLoadingProxies().UseNpgsql(conectionString, npgsqlOptions =>
    {
        npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
    })
);

builder.Services
.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<UserService>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<UserIdMiddleware>();
builder.Services.AddScoped<ErrorHandler>();

builder.Services.AddIdentity<Users, IdentityRole>()
.AddEntityFrameworkStores<FinaceContext>()
.AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();

var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("ERSDTFYGUHIJN23W4E5RT6Y7U8I930REHUSDDSCCDLEWUIEFU"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("MyFinance", policy =>
//     policy.AddRequirements(new MyFinance("")));
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
