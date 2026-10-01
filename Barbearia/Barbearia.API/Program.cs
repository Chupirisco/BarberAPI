using Barbearia.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Controllers (caso você use Controllers em vez de Minimal APIs)
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infraestrutura: AppDbContext + Repositórios (tudo que está no Infra.IoC)
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();