# 💈 BarberFlow

Sistema completo de gerenciamento para barbearias, desenvolvido com foco em agendamentos, controle de agenda, autenticação de usuários e organização de fluxo operacional.

> Projeto Full Stack em constante evolução, focado em arquitetura limpa, boas práticas e regras reais de negócio.

---

#  Sobre o Projeto

O **BarberFlow** é uma aplicação web desenvolvida para simular um sistema real de gestão de barbearias, permitindo que clientes realizem agendamentos online enquanto barbeiros e administradores gerenciam serviços, horários e usuários.

O projeto foi criado com o objetivo de evoluir conhecimentos em:

- Desenvolvimento Full Stack
- APIs REST com ASP.NET Core
- React + consumo de APIs
- Entity Framework Core
- Arquitetura em camadas
- JWT Authentication
- Regras de negócio reais
- Controle de agenda e conflitos de horários

---

#  Funcionalidades

## 👤 Cliente
- Cadastro e login
- Autenticação JWT
- Escolha de serviços
- Escolha de profissionais
- Visualização de horários disponíveis
- Criação de agendamentos
- Visualização de agendamentos
- Cancelamento de agendamentos

---

## ✂️ Profissional (Barbeiro)
- Visualização da agenda diária
- Horários ocupados e livres
- Visualização de informações do cliente
- Bloqueio de horários
- Desbloqueio de horários
- Controle de disponibilidade
- Regras para impedir conflitos de agenda

---

## 🛠️ Administrador
- Gerenciamento de serviços
- Cadastro, edição e exclusão de serviços
- Gerenciamento de usuários
- Controle de roles/permissões

---

#  Regras de Negócio Implementadas

- Impedimento de conflitos de horário
- Validação de horários disponíveis
- Bloqueio de horários da agenda
- Regras de cancelamento
- Controle de acesso por roles:
  - Admin
  - Professional
  - Client
- Controle de horários passados
- Arredondamento automático para slots de 30 minutos

---

#  Arquitetura

O projeto segue separação em camadas:

```text
BarberFlow.API
BarberFlow.Application
BarberFlow.Domain
BarberFlow.Infrastructure
BarberFlow.Front

```
Tecnologias Utilizadas
Backend
C#
.NET 8
ASP.NET Core Web API
Entity Framework Core
SQL Server
JWT Authentication
FluentValidation

Frontend
React
Vite
Tailwind CSS
Axios
React Router DOM

🗂️ Estrutura do Projeto
Backend
Controllers
Services
DTOs
Entities
DbContext
JWT Authentication
Regras de negócio
Frontend
Sistema de rotas privadas
Controle de autenticação
Dashboard do barbeiro
Fluxo de agendamento por etapas
Integração completa com API
Sistema de Agenda

O sistema trabalha com slots de 30 minutos e possui:

Controle de disponibilidade
Bloqueio manual de horários
Conflito automático de agenda
Cancelamento de horários
Visualização em tempo real da agenda

Autenticação
O projeto utiliza autenticação via JWT com controle de permissões baseado em roles.

Roles disponíveis:
Admin
Professional
Client

Pré-requisitos
.NET 8
Node.js
SQL Server

Backend

# Restaurar dependências
dotnet restore

# Executar migrations
dotnet ef database update

# Rodar API
dotnet run --project BarberFlow.API


 Frontend

 cd BarberFlow-Front

npm install

npm run dev

Objetivo do Projeto

Este projeto foi desenvolvido com foco em aprendizado avançado de desenvolvimento Full Stack, aplicando conceitos utilizados em sistemas reais de produção, especialmente em:

Arquitetura
Organização de código
Escalabilidade
Segurança
Regras de negócio
Experiência do usuário

Desenvolvido por Eliel Costa.
