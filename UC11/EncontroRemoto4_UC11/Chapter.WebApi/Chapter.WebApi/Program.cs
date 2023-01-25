using Chapter.WebApi.Contexts;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ChapterContext, ChapterContext>();
builder.Services.AddTransient<ILivroRepository, LivroRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();

//Adicionado servi�o de CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//Adicionado servi�o de JwtBearer : forma de autentica��o
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

//define os par�metros de valida��o do token
.AddJwtBearer("JwtBearer", options =>
{
      options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
      {
          //valida quem est� solicitando
          ValidateIssuer = true,
          //valida quem est� recebendo
          ValidateAudience = true,
          //define se o tempo de expira��o ser� valido
          ValidateLifetime = true,
          //forma de criptografia e ainda valida a chave de autentica��o
          IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter.WebApi-chave-autenticacao")),
          //valida o tempo de autentica��o do token
          ClockSkew = TimeSpan.FromMinutes(30),
          //nome do issuer, de onde est� vindo
          ValidIssuer = "Chapter.WebApi",
          //nome do audience, para onde indo
          ValidAudience = "Chapter.WebApi"

      };
});


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

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
