# Mouts.SalesDeveloper.Api

API REST para gerenciamento de vendas, desenvolvida em .NET 8, utilizando PostgreSQL, Docker e arquitetura DDD.

---

## ✅ Funcionalidades

- Cadastro, consulta, edição e cancelamento de vendas (CRUD completo).
- Cálculo de desconto automático:
  - 4 ou mais itens iguais: 10% de desconto.
  - Entre 10 e 20 itens iguais: 20% de desconto.
  - Máximo permitido: 20 unidades por item.
- Registro de eventos via log:
  - SaleCreated
  - SaleModified
  - SaleCancelled
  - ItemCancelled
- Healthcheck configurado.
- Estrutura separada em:
  - Application
  - Domain
  - Infrastructure
  - Api

---

## 🎯 Requisitos

- .NET 8 SDK
- Docker + Docker Compose
- PostgreSQL 13+

---

## ⚙️ Como Executar

### Local (Visual Studio ou CLI)

1. Criar o arquivo `appsettings.Local.json` na pasta `Mouts.SalesDeveloper.Api`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=sales_db;Username=sales_user;Password=Sales@123"
     }
   }
   ```

2. Criar as tabelas executando as migrations:

   ```bash
   dotnet ef database update --project Mouts.SalesDeveloper.Infrastructure
   ```

3. Executar o projeto via Visual Studio ou via CLI:

   ```bash
   dotnet run --project Mouts.SalesDeveloper.Api
   ```

4. Acessar:

   ```
   http://localhost:5119/swagger
   ```

---

### Docker Compose

1. Na pasta `src/Docker`, executar:

   ```bash
   docker-compose down -v --remove-orphans
   docker-compose up --build
   ```

2. Acessar:

   - API Swagger: http://localhost:5000/swagger
   - Banco de dados:  
     - Host: localhost  
     - Porta: 5432  
     - Database: sales_db  
     - User: sales_user  
     - Password: Sales@123  

3. Healthcheck disponível em:

   ```
   http://localhost:5000/health
   ```

---

## 📑 Endpoints Principais

- `POST /api/sales` → Criar venda
- `GET /api/sales` → Listar vendas
- `GET /api/sales/{id}` → Detalhe da venda
- `PUT /api/sales/{id}` → Atualizar venda
- `POST /api/sales/{id}/cancel` → Cancelar venda
- `GET /api/sales/paginated` → Listar vendas paginadas com paginação

---

## 📂 Estrutura do Projeto

- **Mouts.SalesDeveloper.Api** → Controllers, Middlewares, Configuração geral
- **Mouts.SalesDeveloper.Application** → Commands, Handlers, DTOs, Validations
- **Mouts.SalesDeveloper.Domain** → Entidades, Regras de negócio, Validações
- **Mouts.SalesDeveloper.Infrastructure** → Contexto EF, Repositórios, Migrations

---

## ✅ Observação Final

Projeto estruturado conforme boas práticas de:

- Domain-Driven Design (DDD)
- Clean Code
- Testabilidade
- Dockerização para facilitar setup e integração