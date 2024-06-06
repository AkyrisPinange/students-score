using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudantScore.Data;
using StudantScore.Repositories;
using StudantScore.Services;
using StudantScore.Strategies;
using StudantScore.utils;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register a DB in memory with the path to the CSV file
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseInMemoryDatabase("SchoolDatabase"));

// Register the CsvFileReader with the path to the CSV file
builder.Services.AddSingleton<CsvFileReader>(sp => new CsvFileReader("mock/alunos.csv"));


builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();

builder.Services.AddScoped<ISortingStrategy, BubbleSortStrategy>();
builder.Services.AddScoped<ISortingStrategy, QuickSortStrategy>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
       .RequireAuthenticatedUser()
       .Build();

    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAssertion(_ => true)
        .Build();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Use the CORS policy
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

// loads data from cvs simulating the real database
InitializeDatabase(app);

app.Run();

void InitializeDatabase(IApplicationBuilder app)
{
        using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {

            var context = serviceScope.ServiceProvider.GetRequiredService<SchoolContext>();
            var csvFileReader = serviceScope.ServiceProvider.GetRequiredService<CsvFileReader>();

            if (!context.Alunos.Any())
            {
                var alunos = csvFileReader.ReadAlunosFromCsv();
                context.Alunos.AddRange(alunos);
                context.SaveChanges();
            }
        }  
}