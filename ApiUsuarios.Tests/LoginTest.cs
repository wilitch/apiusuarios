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

namespace ApiUsuarios.Tests
{
    /// <summary>
    /// Classe de teste para o ENDPOINT /api/Login
    /// </summary>
    public class LoginTest
    {
        [Fact]
        public async void Post_Login_Returns_Ok()
        {
            var client = new WebApplicationFactory<Program>().CreateClient();
            var faker = new Faker("pt_BR");

            #region Criando um usuário na API

            var criarContaModel = new CriarContaModel()
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste1234"
            };

            var contentCriarConta = new StringContent(JsonConvert.SerializeObject(criarContaModel),
                Encoding.UTF8, "application/json");

            await client.PostAsync("/api/CriarConta", contentCriarConta);

            #endregion

            #region Autenticando o usuário

            var loginModel = new LoginModel()
            {
                Email = criarContaModel.Email,
                Senha = criarContaModel.Senha
            };

            var contentLogin = new StringContent(JsonConvert.SerializeObject(loginModel),
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Login", contentLogin);

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async void Post_Login_Returns_Unauthorized()
        {
            var loginModel = new LoginModel()
            {
                Email = "teste@teste.com.br",
                Senha = "@Teste4321"
            };

            var client = new WebApplicationFactory<Program>().CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(loginModel),
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Login", content);

            response.StatusCode
               .Should()
               .Be(HttpStatusCode.Unauthorized);
        }
    }
}



