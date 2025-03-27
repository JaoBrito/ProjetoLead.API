# Projeto Lead (Backend) - ASP.NET Core Web API com Entity Framework e SQLite
A API foi criada para gerenciar operações de CRUD com o banco de dados SQLite.

## Clonando o Repositório
Clone este repositório para a sua máquina local:

`git clone https://github.com/seuusuario/seurepositorio.git`

Entre no diretório do projeto:

`cd seurepositorio`

## Configuração do Banco de Dados
Este projeto usa o SQLite como banco de dados. O Entity Framework Core foi configurado para utilizar um banco em memória ou um arquivo SQLite.

Criação do banco de dados: Ao rodar o projeto pela primeira vez, o banco de dados será criado automaticamente com base na configuração definida no DbContext.

### Aplicando Migrations (caso o banco não seja criado automaticamente):
`dotnet ef database update`

## Configuração do Projeto
### Instalar as dependências do projeto
Execute o comando abaixo para restaurar os pacotes NuGet do projeto:

`dotnet restore`

### Configuração de appsettings.json
Certifique-se de que a string de conexão no arquivo appsettings.json está configurada corretamente para o SQLite:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=database.db"
  }
}
```


Isso irá configurar a conexão com o banco SQLite localizado no arquivo database.db.

### Executando o Projeto
Para rodar o projeto localmente, execute o seguinte comando:

`dotnet run`

A API estará disponível em https://localhost:5001 ou http://localhost:5000 dependendo da configuração de sua máquina.

## Endpoints Disponíveis
A API oferece os seguintes endpoints:

`GET /api/Leads:` Retorna todos os Leads.

`GET /api/Leads/{id}`: Retorna um Lead específico.

`POST /api/Leads`: Cria um novo Lead.

`PUT /api/Leads/{id}`: Atualiza um Lead existente.

`DELETE /api/Leads/{id}`: Deleta um Lead.