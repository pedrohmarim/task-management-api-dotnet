# 🚀 Task Management API (.NET 8)

API RESTful para gerenciamento de tarefas (ToDo), desenvolvida com foco em **DDD (Domain-Driven Design)**, **Clean Architecture** e boas práticas de engenharia de software.

---

## 📌 Descrição

Esta API permite criar, consultar, atualizar e remover tarefas, garantindo regras de negócio consistentes e separação de responsabilidades entre as camadas da aplicação.

---

## 🧠 Arquitetura

O projeto segue os princípios de:

- Domain-Driven Design (DDD)
- Clean Architecture
- SOLID
- Separation of Concerns

### 📂 Estrutura

```
TaskManager.API           → Camada de apresentação (Controllers, Swagger)
TaskManager.Application   → Casos de uso (Services, DTOs)
TaskManager.Domain        → Regras de negócio (Entities, Enums)
TaskManager.Infrastructure→ Persistência (EF Core, Repositories, UoW)
TaskManager.Tests         → Testes unitários e integração
```

---

## ⚙️ Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SQLite)
- xUnit
- Moq
- FluentAssertions
- Swagger (OpenAPI)
- Docker

---

## 📦 Funcionalidades

- Criar tarefa
- Listar tarefas
- Filtrar por status e data
- Atualizar tarefa
- Remover tarefa

---

## 🧾 Modelo de Tarefa

```json
{
  "id": "guid",
  "title": "string",
  "description": "string",
  "status": "Pending | InProgress | Done",
  "dueDate": "datetime"
}
```

---

## 🚀 Como rodar o projeto

### 🔹 Pré-requisitos

- .NET 8 SDK
- Docker (opcional)

---

### ▶️ Rodar localmente

```bash
dotnet restore
dotnet build
dotnet run --project TaskManager.API
```

---

### 🌐 Acessar Swagger

```
http://localhost:8080/swagger
```

ou

```
http://localhost:5000/swagger
```

---

## 🧪 Executar testes

```bash
dotnet test
```

---

## 🐳 Rodar com Docker

### Build da imagem

```bash
docker build -t taskmanager-api .
```

### Rodar container

```bash
docker run -d -p 8080:8080 taskmanager-api
```

### Acessar

```
http://localhost:8080/swagger
```

---

## 🧠 Regras de Negócio

- Título não pode ser vazio
- Data de vencimento não pode ser no passado
- Status segue fluxo:
  - Pending → InProgress → Done

---

## 🧪 Testes

O projeto possui cobertura de testes para:

- Entidades (Domain)
- Services (Application)
- Controllers (Integração)

---

## 📈 Diferenciais

- Arquitetura limpa e escalável
- Separação de camadas (DDD)
- Uso de Repository + UnitOfWork
- Testes unitários e de integração
- Swagger documentado
- Pronto para deploy com Docker

---

## 👨‍💻 Autor

Pedro Cavelani

---

## 📌 Observações

Projeto desenvolvido como desafio técnico, com foco em boas práticas de backend e qualidade de código.
