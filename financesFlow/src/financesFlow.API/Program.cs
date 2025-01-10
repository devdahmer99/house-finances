using System.Text;
using financesFlow.API.Filtros;
using financesFlow.API.Middleware;
using financesFlow.Aplicacao;
using financesFlow.Infra;
using financesFlow.Infra.Migrations;
using financesFlow.Infra.Seguranca;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
}); ;
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
builder.Services.AddCors(options => 
     options.AddPolicy("AllowSpecificOrigins", policy =>
     {
         policy.WithOrigins("https://localhost:3000/", "http://localhost:3000/")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .WithExposedHeaders("Content-Disposition")
               .SetIsOriginAllowed(_ => true);
     }));

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigins");

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);
}