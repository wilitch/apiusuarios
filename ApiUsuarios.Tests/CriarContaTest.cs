using ApiUsuarios.Application.Models;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiUsuarios.Tests
{
    /// <summary>
    /// Classe de teste para o ENDPOINT /api/CriarConta
    /// </summary>
    public class CriarContaTest
    {
        [Fact]
        public async void Post_CriarConta_Returns_Created()
        {
            var faker = new Faker("pt_BR");

            //gerando os dados para o teste
            var model = new CriarContaModel()
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste1234"
            };

            //criando um objeto capaz de executar chamadas para os serviços da API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //serializando para JSON os dados que serão enviados para a API
            var content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            //executando o serviço da API que será testado
            var response = await client.PostAsync("/api/CriarConta", content);

            //verificando se a API retornou sucesso (201 - CREATED)
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.Created);
        }

        [Fact]
        public async void Post_CriarConta_Returns_BadRequest()
        {
            var faker = new Faker("pt_BR");

            //gerando os dados para o teste
            var model = new CriarContaModel()
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste1234"
            };

            //criando um objeto capaz de executar chamadas para os serviços da API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //serializando para JSON os dados que serão enviados para a API
            var content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            //executando o cadastro do usuário
            await client.PostAsync("/api/CriarConta", content);

            //tentando cadastrar o mesmo usuário novamente
            var response = await client.PostAsync("/api/CriarConta", content);

            //verificando se a API retornou sucesso (400 - BAD REQUEST)
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.BadRequest);
        }
    }
}



