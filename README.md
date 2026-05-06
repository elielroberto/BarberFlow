# 💈 BarberFlow

Sistema Full Stack para gerenciamento de barbearias, desenvolvido com foco em agendamentos online, controle de agenda, autenticação de usuários, permissões por perfil e regras reais de negócio.

> Projeto em constante evolução, criado com o objetivo de aplicar boas práticas de desenvolvimento, arquitetura em camadas e integração entre API REST e frontend React.

---

##  Sobre o Projeto

O **BarberFlow** é uma aplicação web que simula um sistema real de gestão para barbearias.

A plataforma permite que clientes realizem agendamentos online, enquanto profissionais e administradores conseguem gerenciar serviços, horários, usuários e disponibilidade da agenda.

O projeto foi desenvolvido para consolidar conhecimentos em:

- Desenvolvimento Full Stack
- ASP.NET Core Web API
- React com consumo de API
- Entity Framework Core
- SQL Server
- Autenticação JWT
- Controle de acesso por roles
- Arquitetura em camadas
- Regras de negócio reais
- Controle de agenda e conflitos de horários

---

##  Funcionalidades

### 👤 Cliente

- Cadastro e login
- Autenticação via JWT
- Escolha de serviços
- Escolha de profissionais
- Visualização de horários disponíveis
- Criação de agendamentos
- Visualização dos próprios agendamentos
- Cancelamento de agendamentos

---

### 💈 Profissional

- Visualização da própria agenda
- Consulta de horários ocupados e livres
- Visualização das informações dos clientes agendados
- Bloqueio manual de horários
- Remoção de bloqueios de horários
- Controle de disponibilidade
- Regras para impedir conflitos de agenda

---

###  Administrador

- Gerenciamento de serviços
- Cadastro de serviços
- Edição de serviços
- Desativação de serviços
- Gerenciamento de usuários
- Alteração de roles/permissões

---

##  Regras de Negócio Implementadas

- Impedimento de conflito entre agendamentos
- Impedimento de agendamento em horários bloqueados
- Validação de horários disponíveis
- Controle de horários passados
- Bloqueio manual de agenda pelo profissional
- Cancelamento de agendamentos
- Controle de permissões por perfil:
  - `Admin`
  - `Professional`
  - `Client`
- Agenda baseada em slots de 30 minutos
- Cálculo automático da duração do atendimento com base no serviço escolhido

---

##  Arquitetura

O projeto segue uma organização em camadas, separando responsabilidades entre API, aplicação, domínio, infraestrutura e frontend.

```text
BarberFlow.API
BarberFlow.Application
BarberFlow.Domain
BarberFlow.Infrastructure
BarberFlow-Front
```

### Responsabilidades das camadas

```text
BarberFlow.API
```

Camada responsável pelos controllers, autenticação, configuração da API e exposição dos endpoints.

```text
BarberFlow.Application
```

Camada responsável pelos serviços da aplicação, DTOs, interfaces, validações e regras de uso.

```text
BarberFlow.Domain
```

Camada responsável pelas entidades e enums do domínio.

```text
BarberFlow.Infrastructure
```

Camada responsável pelo acesso ao banco de dados, Entity Framework Core e AppDbContext.

```text
BarberFlow-Front
```

Frontend desenvolvido em React, responsável pela interface do usuário e consumo da API.

## 🧭 Principais Endpoints

### Autenticação

```http
POST /api/auth/register
POST /api/auth/login
```

### Usuários

```http
GET /api/users
GET /api/users/{id}
PUT /api/users/role
```

### Serviços

```http
GET /api/services
GET /api/services/{id}
POST /api/services
PUT /api/services/{id}
DELETE /api/services/{id}
```

### Profissionais

```http
GET /api/professionals
GET /api/professionals/me
```

### Agendamentos

```http
POST /api/appointments
GET /api/appointments/me
PUT /api/appointments/{id}/cancel
```

### Agenda

```http
GET /api/schedules/available
GET /api/schedules/day
GET /api/schedules/me
```

### Bloqueio de Horários

```http
POST /api/blocked-times
DELETE /api/blocked-times/{id}
```

---

##  Tecnologias Utilizadas

### Backend

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- FluentValidation
- BCrypt

### Frontend

- React
- Vite
- Tailwind CSS
- Axios
- React Router DOM

### Ferramentas

- Visual Studio
- Visual Studio Code
- SQL Server Management Studio
- Git
- GitHub
- Swagger

---

##  Autenticação e Autorização

O projeto utiliza autenticação via **JWT**, com controle de permissões baseado em roles.

Perfis disponíveis:

```text
Admin
Professional
Client
```

Cada perfil possui acesso limitado às funcionalidades correspondentes.

Exemplo:

- Clientes podem criar e visualizar seus agendamentos.
- Profissionais podem visualizar agenda e bloquear horários.
- Administradores podem gerenciar usuários e serviços.

---

##  Sistema de Agenda

O sistema trabalha com horários divididos em slots de 30 minutos.

A agenda possui:

- Listagem de horários disponíveis
- Controle de horários ocupados
- Bloqueio manual de horários
- Validação contra conflitos de agendamento
- Validação contra horários passados
- Cancelamento de agendamentos
- Visualização da agenda diária do profissional

---

##  Como Executar o Projeto

### Pré-requisitos

Antes de começar, é necessário ter instalado:

- .NET 8 SDK
- Node.js
- SQL Server
- Git

---

##  Backend

Acesse a pasta do projeto backend:

```bash
cd BarberFlow
```

Restaure as dependências:

```bash
dotnet restore
```

Execute as migrations:

```bash
dotnet ef database update
```

Execute a API:

```bash
dotnet run --project BarberFlow.API
```

A API poderá ser acessada pelo Swagger, conforme a configuração do projeto.

---

##  Frontend

Acesse a pasta do frontend:

```bash
cd BarberFlow-Front
```

Instale as dependências:

```bash
npm install
```

Execute o projeto:

```bash
npm run dev
```

---

##  Demonstração da Aplicação

### Endpoints da API

<img width="837" height="879" alt="Api" src="https://github.com/user-attachments/assets/8df650fa-6f5e-4a7b-b0ce-d2d40c594fb5" />

---

### Login

<img width="1126" height="904" alt="Inicial" src="https://github.com/user-attachments/assets/503776ea-08c1-4424-82eb-725b1695e797" />

---

### Registro

<img width="1132" height="902" alt="registro" src="https://github.com/user-attachments/assets/2713dee1-88cf-4444-b2f5-fb4e6b5305d7" />

---

### Painel Administrativo

<img width="1126" height="897" alt="inicial Admin" src="https://github.com/user-attachments/assets/68fd6442-00a6-4ab1-b92d-594fe04c3a02" />

---

### Gerenciamento de Usuários

<img width="1129" height="895" alt="PainelUsuariosAdmin" src="https://github.com/user-attachments/assets/5e741466-2c2d-44bf-9173-5d2e11290733" />

---

### Criação de Serviços

<img width="1137" height="898" alt="CriarServiçoAdmin" src="https://github.com/user-attachments/assets/f696d26a-a68d-4301-9fa7-bb2567f9bf13" />

---

### Serviço Cadastrado

<img width="1126" height="898" alt="ServiçoCadastradoAdmin" src="https://github.com/user-attachments/assets/a8d6864d-dddc-4e1f-90dc-94b2b7b0132a" />

---

### Agenda do Profissional

<img width="1009" height="949" alt="AgendaBarbeiro" src="https://github.com/user-attachments/assets/60540464-7460-469f-bc06-3bd53cd828ea" />

---

### Tela Inicial do Cliente

<img width="1023" height="950" alt="AgendarCliente" src="https://github.com/user-attachments/assets/e0907d66-11dd-4d13-b5b2-9ad627b9ca52" />

---

### Escolha do Profissional

<img width="1023" height="950" alt="EscolherProfissional" src="https://github.com/user-attachments/assets/6404540f-3369-42eb-837d-1b0210419ed0" />

---

### Escolha do Horário

<img width="845" height="943" alt="EscolherHorario" src="https://github.com/user-attachments/assets/dbbc68a3-59ee-4a76-8288-761ca0d39a22" />

---

##  Objetivo do Projeto

Este projeto foi desenvolvido como parte da minha evolução profissional em desenvolvimento Full Stack, com foco em aplicar conceitos utilizados em sistemas reais, como:

- Organização de código
- Separação de responsabilidades
- APIs REST
- Autenticação e autorização
- Integração frontend/backend
- Regras de negócio
- Validação de dados
- Controle de agenda
- Experiência do usuário

---

##  Status do Projeto

Projeto em desenvolvimento e evolução contínua.

##  Desenvolvido por

**Eliel Costa**

Projeto desenvolvido para fins de estudo, portfólio e demonstração de habilidades em desenvolvimento Full Stack com .NET e React.
