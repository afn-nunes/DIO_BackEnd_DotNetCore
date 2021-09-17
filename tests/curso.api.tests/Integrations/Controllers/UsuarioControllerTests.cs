using AutoBogus;
using curso.api.models.Usuarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected readonly ITestOutputHelper _output;
        protected readonly HttpClient _httpClient;
        protected RegistroViewModelInput RegistroViewModelInput;
        protected LoginViewModelOutput LoginViewModelOutput;

        public UsuarioControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _output = output;
        }

        public async Task DisposeAsync()
        {
            _httpClient.Dispose();
        }

        public async Task InitializeAsync()
        {
            await Registrar_InformandoUsuarioESenhaNaoExistentes_DeveRetornarCreated();
            await Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucess();
        }

        //Padrão de nomeclatura WhenGivenThen
        //            Quando     Dados                             Entao
        //public void Autenticar_InformandoUsuarioESenhaExistentes_RetornarSucess

        //Padrão de escrita AAA(Arrange, Act, Assert )

        [Fact]
        public async Task  Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucess()
        {
            //Arrange
            var loginViewModelInput = new LoginViewModelInput
            {
                Login = RegistroViewModelInput.Login,                
                Senha = RegistroViewModelInput.Senha
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModelInput), Encoding.UTF8, "application/json");

            //Act
            var HttpClientRequest = await _httpClient.PostAsync("api/v1/usuario/logar", content);
            LoginViewModelOutput = JsonConvert.DeserializeObject<LoginViewModelOutput>(await HttpClientRequest.Content.ReadAsStringAsync());
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, HttpClientRequest.StatusCode);
            Assert.Equal(loginViewModelInput.Login, LoginViewModelOutput.Usuario.Login);
            Assert.NotNull(LoginViewModelOutput.Token);
            _output.WriteLine(LoginViewModelOutput.Token);
            
        }

        [Fact]
        public async Task Registrar_InformandoUsuarioESenhaNaoExistentes_DeveRetornarCreated()
        {
            //Arrange
            RegistroViewModelInput = new AutoFaker<RegistroViewModelInput>()
                                                    .RuleFor(p => p.Email, faker => faker.Person.Email);

            StringContent content = new StringContent(JsonConvert.SerializeObject(RegistroViewModelInput), Encoding.UTF8, "application/json");

            //Act
            var HttpClientRequest = await _httpClient.PostAsync("api/v1/usuario/registrar", content);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, HttpClientRequest.StatusCode);
        }
    }
}
