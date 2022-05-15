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

Configurar o jew no startup, tanto no app quanto no settings, 

Nas configurações do swagger, configurar para ser possível aplicar a autorização no swagger 

Adicionar Authorization na controller, para que só seja possível realizar as pesquisas com o usuário logado 

Criar uma seção no appsettings com a chave de autenticação

# Configurando a persistência de dados
Biblioteca Microsoft EntityFrameworkCore 

Biblioteca Microsoft EntityFrameworkCore.Relational  

Biblioteca Microsoft EntityFrameworkCore.SQLServer 

Configurar as entidades, mapping e contexto

## Configurando as migrations
Biblioteca Microsoft EntityFrameworkCore.tools 

Microsoft.EntityFrameworkCore.Design

Criar a pasta Configuration e a classe DbFactoryDbContext 


Acessar tools-manager-package console 

Executar o comando Add-Migration Base-inicial













