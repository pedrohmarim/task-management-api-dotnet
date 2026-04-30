# 🚀 Task Management API (.NET 8)

API RESTful para gerenciamento de tarefas (ToDo), desenvolvida com **DDD, Clean Architecture e SOLID**.

---

## 📌 Funcionalidades

- Criar, listar, atualizar e remover tarefas
- Filtrar por status e data de vencimento

---

## 🧠 Arquitetura

O projeto segue os princípios de:

- Domain-Driven Design (DDD)
- Clean Architecture
- SOLID
- Separation of Concerns

### 📂 Estrutura

```
TaskManager.API            → Controllers / Swagger
TaskManager.Application    → Services / DTOs
TaskManager.Domain         → Regras de negócio
TaskManager.Infrastructure → EF Core / Repositories / UoW
TaskManager.Tests          → Testes
```

---

## ⚙️ Tecnologias

- .NET 8 / ASP.NET Core
- Entity Framework Core (SQLite)
- xUnit, Moq, FluentAssertions
- Swagger
- Docker

---

## 🗄️ Banco de Dados

A aplicação utiliza **SQLite** como banco de dados relacional, escolhido por sua simplicidade e facilidade de execução local, sem necessidade de instalação de servidor.

- Ideal para ambientes de desenvolvimento e testes
- Banco baseado em arquivo (`.db`)
- Configuração leve e rápida

O banco é criado automaticamente via **Entity Framework Core Migrations** ao iniciar a aplicação.

---

## 🚀 Como rodar

### ▶️ Local

```bash id="9m1n7r"
dotnet restore
dotnet build
dotnet run --project TaskManager.API
```

Swagger:

```
http://localhost:8080/swagger
```

---

### 🐳 Docker

```bash id="y5bq7u"
docker build -t taskmanager-api .
docker run -d -p 8080:8080 taskmanager-api
```

Swagger:

```
http://localhost:8080/swagger
```

---

## 🧪 Testes

```bash id="r3p6hz"
dotnet test
```

---

## 🧠 Regras de Negócio

- Título obrigatório
- Data não pode ser no passado
- Fluxo: Pending → InProgress → Done

---

## 📈 Diferenciais

- DDD + Clean Architecture
- Repository + UnitOfWork
- Testes unitários e integração
- Pronto para Docker

---

## 👨‍💻 Autor

Pedro Cavelani

---

## 📌 Observações

Projeto desenvolvido como desafio técnico, com foco em boas práticas de backend e qualidade de código.
