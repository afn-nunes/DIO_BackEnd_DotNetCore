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



