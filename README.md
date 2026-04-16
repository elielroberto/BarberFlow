# 💈 BarberFlow

Sistema de gestão para barbearias, desenvolvido com foco em organização de atendimentos, serviços e fluxo de clientes.

>  Projeto em desenvolvimento — novas funcionalidades estão sendo implementadas continuamente.

---

##  Sobre o Projeto

O **BarberFlow** é uma aplicação web criada para facilitar o gerenciamento de uma barbearia, permitindo o controle de serviços, organização de dados e estruturação de uma API escalável.

Este projeto faz parte do meu processo de evolução como desenvolvedor Full Stack, aplicando boas práticas de arquitetura, organização de código e desenvolvimento com .NET.

---

Este projeto tem como objetivo:

Praticar desenvolvimento backend com .NET
Aplicar conceitos de arquitetura limpa
Evoluir boas práticas (SOLID, separação de responsabilidades)
Servir como projeto de portfólio

---

##  Tecnologias Utilizadas

###  Backend
- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core

###  Arquitetura
- Separação em camadas:
  - API
  - Application
  - Domain
  - Infra (em evolução)

---

##  Funcionalidades Implementadas

-  Estrutura inicial da API
-  Criação de entidade **Service**
-  Endpoint para buscar serviço por ID
-  DTOs para resposta e atualização
-  Configuração do DbContext
-  Integração com banco de dados
-  Organização por camadas (DDD básico)

---

##  Em Desenvolvimento

-  Atualização (Update) de serviços
-  Validações mais robustas
-  Padronização de respostas da API
-  Implementação de testes unitários
-  Novas entidades (Clientes, Agendamentos, etc.)
-  Autenticação e autorização (JWT)

---

##  Como executar o projeto

### Pré-requisitos:
- .NET 8 instalado
- Banco de dados configurado (SQL Server ou outro compatível com EF)

### Passos:

```bash
# Clonar o repositório

# Entrar na pasta
cd BarberFlow

# Restaurar dependências
dotnet restore

# Rodar o projeto
dotnet run --project BarberFlow.API
