using financesFlow.API.Filtros;
using financesFlow.API.Middleware;
using financesFlow.Aplicacao;
using financesFlow.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDeExcecao)));
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseQueryStrings = true;
    options.LowercaseUrls = true;
});

builder.Services.AdicionarInfra(builder.Configuration);
builder.Services.AdicionarAplicacao();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
