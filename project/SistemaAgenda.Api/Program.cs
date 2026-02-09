using SistemaAgenda.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Inyecciones
builder.Services.AddScoped<IAgendaService, AgendaServiceImpl>();

// Add services to the container.
builder.Services.AddControllers();

// dotnet build "SistemaAgenda.Api.csproj"

// Swagger/OpenAPI (Swashbuckle)
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
