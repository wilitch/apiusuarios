using ApiUsuarios.Application.Interfaces;
using ApiUsuarios.Application.Services;
using ApiUsuarios.Domain.Interfaces.Producers;
using ApiUsuarios.Domain.Interfaces.Repositories;
using ApiUsuarios.Domain.Interfaces.Services;
using ApiUsuarios.Domain.Services;
using ApiUsuarios.Infra.Data.Repositories;
using ApiUsuarios.Infra.Messages.Services;
using ApiUsuarios.Infra.RabbitMQ.Consumers;
using ApiUsuarios.Infra.RabbitMQ.Producers;
using ApiUsuarios.Services.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adicionando a configuração para autenticação com JWT
JwtConfiguration.Configure(builder);

//configurar as injeções de dependência
builder.Services.AddTransient<IUsuarioAppService, UsuarioAppService>();
builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IMessageQueueProducer, MessageQueueProducer>();

//registrando o consumer do RabbitMQ
builder.Services.AddHostedService<MessageQueueConsumer>();

//configurar o AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*
    definindo a política de acesso da API, configurando quais
    aplicações poderão fazer chamadas para a nossa API (ALL)
*/
builder.Services.AddCors(
       s => s.AddPolicy("CorsSetup", builder =>
       {
           builder.AllowAnyOrigin() //qualquer projeto/origem pode acessar a API
                  .AllowAnyMethod() //qualquer método (POST, PUT, DELETE, GET)
                  .AllowAnyHeader(); //qualquer informação de cabeçalho
       })
   );

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsSetup"); //ativando o CORS

app.Run();

public partial class Program { }



