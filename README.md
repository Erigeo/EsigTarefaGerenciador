# EsigTaskApi - ASP.NET Web API
- Para visualizar o frontend com o resto dos passos necessários para usufruir da aplicação completa acesse: https://github.com/Erigeo/EsigTasksFront

## Descrição
Esta API foi desenvolvida com base no que foi proposto na tarefa de estágio fullstack da Esig Group. Ela fornece endpoints para autentição de usuário, register e para realização de CRUD para tasks e usuários. 

## Tecnologias Utilizadas
- **ASP.NET Web API**
- **.NET Core** (versão 7)
- **Entity Framework Core** (se estiver usando para persistência de dados)
- Oracle DB free version
- **Swagger**
- JWT authentication

## Pré-requisitos
Antes de começar, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:
- [.NET SDK](https://dotnet.microsoft.com/download) (versão x.x.x)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/) com as extensões C#
- [Oracle DB](https://www.oracle.com/br/database/free/ ou outro banco de dados configurado, conforme sua implementação.

## Instalação
1. Clone o repositório para sua máquina local:
    ```bash
    git clone https://github.com/Erigeo/EsigTarefaGerenciador
    ```

2. Navegue até a pasta do projeto:
    ```bash
    cd EsigGestãoDeTarefasApp
    ```

3. Restaure os pacotes NuGet:
    ```bash
    dotnet restore
    ```

4. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "User Id=SEU_USER;Password=SUA_SENHA;Data Source=localhost:1521/SEU_SERVICE;"
      }
    }
    ```

5. Execute as migrações do banco de dados (caso esteja usando Entity Framework):
    ```bash
    dotnet ef database update
    ```

6. Compile e execute o projeto:
    ```bash
    dotnet run
    ```

A API estará disponível no endereço: `https://localhost:7281`.

7. Ao dar run na Api, uma página referente ao Swagger será aberta com todos os endpoints relacionados a Api.

8. Para continuar com as etapas acessar: https://github.com/Erigeo/EsigTasksFront 

## Autenticação
Todos os endpoints que não estão relacionados a Authentication é necessário o uso do JWT token obtido após o login para realizar as requisições.

## Documentação via Swagger
Esta API possui documentação via Swagger. Para acessá-la, inicie o projeto e abra o navegador no endereço:

