using Controle_Financeiro___Back.Data;
using Controle_Financeiro___Back.Models;
using Controle_Financeiro___Back.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddScoped<SingUpService>();

builder.Services.AddIdentity<Users, IdentityRole>()
.AddEntityFrameworkStores<FinaceContext>()
.AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
