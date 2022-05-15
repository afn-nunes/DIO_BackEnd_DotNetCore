# DIO_BackEnd_DotNetCore
Configuração da arquitetura back-end com .NET Core - Curso Digital Innovation One

# Configurando o Swagger
Primeiramente marcar nas propriedades do projeto na aba build, a opção de geração do xml na pasta raiz do projeto
Depois instalar o pacote do swagger clicando em dependências, gerenciar pacotes nugget  

Swashbuckle.AspNetCore.Swagger  

Swashbuckle.AspNetCore.SwaggerUI  

Adicionar as configurações do wagger em startup.cs  

# Annnotations
Instalar Swashbuckle.AspNetCore.Annotations

# ConfigureApiBehaviorOptions
Adicionar no startup ConfigureApiBehaviorOptions mudando a propriedade SuppressModelStateInvalidFilter para True  
  
Criar no projeto a pasta Filter para interceptar as badRequests e passar um model state customizado

Na controller, passar a modelState no cabeçalho do método - [ValidacaoModelStateCustomizado]

# Configurando as proteções da API com jwt
Biblioteca microsoft.AspnetCore.Authentication 

Biblioteca microsoft.AspnetCore.Authentication.JWT 

Criar uma seção no appsettings com a chave de autenticação

 ```
var secret = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtConfigurations:Secret").Value);
services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secret),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});```






