using StreamAnalytics.Ingest.BackgroundService;
using StreamAnalytics.Ingest.System.Consumer;
using StreamAnalytics.Ingest.System.Dataflow;
using StreamAnalytics.Ingest.System.Producer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<ConsumerService>();
builder.Services.AddSingleton<OpcIngestRequestDataConsumer>();
builder.Services.AddSingleton<IngestPipeline>();
builder.Services.AddSingleton<OpcIngestSinkDataProducer>();
builder.Services.AddScoped<OpcIngestSourceDataProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();

app.Run();
