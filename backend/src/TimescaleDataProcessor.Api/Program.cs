using Microsoft.EntityFrameworkCore;
using TimescaleDataProcessor.Api.Data.Context;
using TimescaleDataProcessor.Api.Parsers;
using TimescaleDataProcessor.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IParserFactory, ParserFactory>();
builder.Services.AddTransient<CsvParser>();
builder.Services.AddTransient<IRecordValidator, RecordValidator>();
var app = builder.Build();

app.Run();
