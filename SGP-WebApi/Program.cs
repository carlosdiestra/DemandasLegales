using Microsoft.EntityFrameworkCore;
using SGP_Application.Contracts;
using SGP_Domain.Rules;
using SGP_Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Configuracion de Base Datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg =>
{
        cfg.RegisterServicesFromAssembly(typeof(SGP_Application.Contracts.Handlers.ResolveJudgmentSecondHandler.Handler).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(SGP_Application.Contracts.Handlers.ResolveJudgmentFirstHandler.Handler).Assembly);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IFirmaRules, FirmaRules>();

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
