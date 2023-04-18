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
    /// Classe de teste para o ENDPOINT /api/RecuperarSenha
    /// </summary>
    public class RecuperarSenhaTest
    {
        [Fact]
        public async void Post_RecuperarSenha_Returns_Ok()
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

            #region recuperando a senha do usuário

            var recuperarSenhaModel = new RecuperarSenhaModel()
            {
                Email = criarContaModel.Email
            };

            var contentRecuperarSenha = new StringContent(JsonConvert.SerializeObject(recuperarSenhaModel),
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/RecuperarSenha", contentRecuperarSenha);

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async void Post_RecuperarSenha_Returns_BadRequest()
        {
            var recuperarSenhaModel = new RecuperarSenhaModel()
            {
                Email = "teste@teste.com.br"
            };

            var client = new WebApplicationFactory<Program>().CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(recuperarSenhaModel),
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/RecuperarSenha", content);

            response.StatusCode
               .Should()
               .Be(HttpStatusCode.BadRequest);
        }
    }
}



