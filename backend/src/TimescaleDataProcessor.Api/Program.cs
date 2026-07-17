using Microsoft.EntityFrameworkCore;
using TimescaleDataProcessor.Api.Data.Context;
using TimescaleDataProcessor.Api.Parsers;
using TimescaleDataProcessor.Api.Services;
using TimescaleDataProcessor.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IParserFactory, ParserFactory>();
builder.Services.AddTransient<CsvParser>();

builder.Services.AddScoped<ITimescaleDataImportService, TimescaleDataImportService>();
builder.Services.AddTransient<IResultCalculator, ResultCalculator>();
builder.Services.AddTransient<IRecordValidator, RecordValidator>();
builder.Services.AddScoped<IResultsService, ResultsService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
