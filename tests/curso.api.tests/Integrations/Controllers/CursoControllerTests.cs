using AutoBogus;
using curso.api.models.Cursos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class CursoControllerTests : UsuarioControllerTests
    {

        protected CursoViewModelInput CursoViewModelInput;

        public CursoControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        { 
        }

        public async Task DisposeAsync()
        {
            _httpClient.Dispose();
        }

        public async Task InitializeAsync()

        {            
            await Registrar_InformandoUmUsuarioAutenticado_DeveRetornarCreated();
            await Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucess();
        }

        //Padrão de nomeclatura WhenGivenThen
        //            Quando     Dados                             Entao
        //public void Autenticar_InformandoUsuarioESenhaExistentes_RetornarSucess

        //Padrão de escrita AAA(Arrange, Act, Assert )

        [Fact]
        public async Task Registrar_InformandoUmUsuarioAutenticado_DeveRetornarCreated()
        {
            //Arrange
            CursoViewModelInput = new AutoFaker<CursoViewModelInput>();
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(CursoViewModelInput), Encoding.UTF8, "application/json");

            //Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var HttpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);
            //Assert            
            Assert.Equal(System.Net.HttpStatusCode.Created, HttpClientRequest.StatusCode);
            Assert.Equal(CursoViewModelInput.Descricao, CursoViewModelInput.Descricao);
        }


        [Fact]
        public async Task Obter__InformandoUmUsuarioAutenticado_DeveRetornarSucesso()
        {
            //Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            //Act          
            var HttpClientRequest = await _httpClient.GetAsync("api/v1/cursos");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, HttpClientRequest.StatusCode);            
        }

        [Fact]
        public async Task Obter_NaoInformandoUmUsuarioAutenticado_DeveRetornarUnauthorized()
        {
            //Arrange

            //Act          
            var HttpClientRequest = await _httpClient.GetAsync("api/v1/cursos");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, HttpClientRequest.StatusCode);
        }
    }
}
