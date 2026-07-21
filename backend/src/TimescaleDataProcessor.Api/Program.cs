using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;
using TimescaleDataProcessor.Api.Data.Context;
using TimescaleDataProcessor.Api.Middleware;
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
builder.Services.AddScoped<IValuesService, ValuesService>();

builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TimescaleDataProcessor",
        Version = "v1",
        Description = "API для работы с timescale данными и результатами их обработки. " +
            "Обеспечивает импорт файлов с валидацией бизнес-требований и расчётом интегральных результатов по всему набору данных, " +
            "а также предоставляет инструменты для анализа накопленных метрик."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);

    options.DescribeAllParametersInCamelCase();
});

builder.Services.AddControllers();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Logging.AddConsole();

var app = builder.Build();

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TimescaleDataProcessor v1");
    });
}

app.MapControllers();

app.MapHealthChecks("/api/health");

app.Run();
