# Projeto Lead (Backend) - ASP.NET Core Web API com Entity Framework e SQLite
A API foi criada para gerenciar opera��es de CRUD com o banco de dados SQLite.

## Clonando o Reposit�rio
Clone este reposit�rio para a sua m�quina local:

`git clone https://github.com/seuusuario/seurepositorio.git`

Entre no diret�rio do projeto:

`cd seurepositorio`

## Configura��o do Banco de Dados
Este projeto usa o SQLite como banco de dados. O Entity Framework Core foi configurado para utilizar um banco em mem�ria ou um arquivo SQLite.

Cria��o do banco de dados: Ao rodar o projeto pela primeira vez, o banco de dados ser� criado automaticamente com base na configura��o definida no DbContext.

### Aplicando Migrations (caso o banco n�o seja criado automaticamente):
`dotnet ef database update`

## Configura��o do Projeto
### Instalar as depend�ncias do projeto
Execute o comando abaixo para restaurar os pacotes NuGet do projeto:

`dotnet restore`

### Configura��o de appsettings.json
Certifique-se de que a string de conex�o no arquivo appsettings.json est� configurada corretamente para o SQLite:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=database.db"
  }
}
```


Isso ir� configurar a conex�o com o banco SQLite localizado no arquivo database.db.

### Executando o Projeto
Para rodar o projeto localmente, execute o seguinte comando:

`dotnet run`

A API estar� dispon�vel em https://localhost:5001 ou http://localhost:5000 dependendo da configura��o de sua m�quina.

## Endpoints Dispon�veis
A API oferece os seguintes endpoints:

`GET /api/Leads:` Retorna todos os Leads.

`GET /api/Leads/{id}`: Retorna um Lead espec�fico.

`POST /api/Leads`: Cria um novo Lead.

`PUT /api/Leads/{id}`: Atualiza um Lead existente.

`DELETE /api/Leads/{id}`: Deleta um Lead.