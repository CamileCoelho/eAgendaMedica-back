# EAgendaMedica

O e-Agenda Médica é uma solução web inovadora para a gestão de cronogramas de clínicas médicas, permitindo um planejamento eficiente e a otimização dos procedimentos.

# Sobre o Projeto

## Características

- Agendamento de consultas e cirurgias
- Gerenciamento de conflitos de agendamento
- Relatórios dinâmicos
- Autenticação segura

## Tecnologias

O projeto e-Agenda Médica utiliza várias tecnologias de ponta no desenvolvimento do frontend e backend.

### Frontend

- Angular `^16.2.0`
- Angular Material `^16.2.12`
- Bootstrap `^5.3.2`

### Backend

- ASP.NET Core `6.0`
- Entity Framework Core `7.13.0`
- AutoMapper `12.0.1`
- FluentValidation `11.8.0`
- Identity `2.2.0`

## Como Utilizar

### Pré-requisitos

Antes de iniciar, certifique-se de atender aos seguintes requisitos:

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [Node.js e npm](https://nodejs.org/en/download/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Instalação

Siga estas etapas simples para iniciar o projeto localmente:

```bash
# Clone o repositório
git clone https://github.com/CamileCoelho/eAgendaMedica-back.git

# Navegue até a pasta do projeto back-end
cd /backend

# Restaure os pacotes NuGet
dotnet restore

# Configure a string de conexão do banco de dados em appsettings.json

# Aplique as migrations do banco de dados
dotnet ef database update

# Inicie a API
dotnet run

# Em um novo terminal, navegue até a pasta do front-end
cd ../frontend

# Instale as dependências do projeto Angular
npm install

# Inicie o aplicativo Angular
ng serve

# O aplicativo estará disponível em http://localhost:4200 
```
## Uso

Após a instalação e execução do projeto, você pode começar a utilizar o e-Agenda Médica para gerenciar o cronograma da clínica. Aqui estão algumas das operações que você pode realizar:

- **Agendar uma nova consulta ou cirurgia**: Navegue até a seção de atividades, escolha a escolhida e preencha os detalhes da atividade médica desejada.
- **Visualizar o cronograma**: Verifique o calendário para uma visão geral das atividades agendadas.
- **Modificar um agendamento existente**: Edite os detalhes de uma atividade médica caso haja alterações.
