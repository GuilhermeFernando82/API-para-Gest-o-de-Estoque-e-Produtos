# API de Gestão de Produtos, Pedidos e Estoques

API RESTful em .NET 8 para gerenciamento de estoque de uma loja de instrumentos musicais. Implementada seguindo Clean Architecture, DDD, TDD, PostgreSQL, autenticação JWT e Swagger.

## Como executar o projeto

### Pré-requisitos

- Rodar os scripts sql que estão na pasta scripts/init_db.sql, para criar as tabelas necessárias.
- .NET 8 SDK
- Docker e Docker Compose (opcional para banco de dados)

### Execução local

1. Restaure os pacotes:
   ```sh
   dotnet restore
   ```
2. Compile a solução:
   ```sh
   dotnet build
   ```
3. Configure o banco de dados PostgreSQL (ajuste a connection string em `src/MeuProjeto.API/appsettings.Development.json` se necessário).
   ```

   ```
4. Rode a API:
   ```sh
   dotnet run --project src/MeuProjeto.API
   ```
5. Acesse a documentação Swagger em: http://localhost:5000/swagger

### Execução com Docker

1. Execute:
   ```sh
   docker-compose up --build
   ```
2. Acesse a API em: http://localhost:5151

### Executando os testes

Para rodar os testes automatizados (TDD):

```sh
dotnet test
```

---

## Fluxo completo da aplicação

### 1. Cadastro de Usuário

Endpoint: `POST /api/auth/register`

Payload exemplo:

```json
{
  "nome": "Administrador",
  "email": "admin@email.com",
  "senha": "123456",
  "tipo": 1 // 1 = Administrador, 2 = Vendedor
}
```

Resposta: Dados do usuário criado.

### 2. Login

Endpoint: `POST /api/auth/login`

Payload exemplo:

```json
{
  "email": "admin@email.com",
  "senha": "123456"
}
```

Resposta:

```json
{
  "token": "<JWT>"
}
```

Use o token JWT no header Authorization: `Bearer <token>` para os próximos endpoints.

### 3. Cadastro de Produto

Endpoint: `POST /api/produto`

Header: `Authorization: Bearer <token de Administrador>`

Payload exemplo:

```json
{
  "nome": "Guitarra Fender Stratocaster Player",
  "descricao": "Corpo em Alder, braço Maple, 3 captadores Single-Coil Player Series. Cor: Sunburst.",
  "preco": 7500.0,
  "categoria": 1 //0 - EquipamentosEsportivos, 1 - InstrumentosMusicais, 2 - ProdutosJardinagem, 3 - AcessoriosPets
}
```

Resposta: Dados do produto criado.

### 4. Cadastro de Estoque

Endpoint: `POST /api/estoque`

Header: `Authorization: Bearer <token de Administrador>`

Payload exemplo:

```json
{
  "produtoId": "<ID do produto>",
  "quantidade": 10,
  "numeroNotaFiscal": "NF123456"
}
```

Resposta: Dados do estoque criado.

### 5. Realizar Pedido

Endpoint: `POST /api/pedido`

Header: `Authorization: Bearer <token de Vendedor>`

Payload exemplo:

```json
{
  "documentoCliente": "12345678900",
  "nomeVendedor": "Vendedor 1",
  "itens": [
    {
      "produtoId": "<ID do produto>",
      "quantidade": 2
    }
  ]
}
```

Resposta: Dados do pedido realizado.

---

## Endpoints principais

- POST /api/auth/register — Cadastro de usuário
- POST /api/auth/login — Login
- GET /api/produto — Listar produtos
- POST /api/produto — Criar produto
- PUT /api/produto/{id} — Atualizar produto
- DELETE /api/produto/{id} — Remover produto
- POST /api/estoque — Adicionar estoque
- POST /api/pedido — Realizar pedido

Consulte o Swagger para exemplos detalhados de payloads e respostas.

---

## Decisões técnicas

- Clean Architecture (API, Application, Domain, Infrastructure)
- DDD nos agregados principais
- Repository Pattern e Use Cases
- Injeção de dependência nativa
- Middleware global para erros
- Logging estruturado
- Autenticação JWT e roles
- Dapper para comunicação com banco
- Testes unitários com xUnit e Moq
- Docker para API e PostgreSQL
