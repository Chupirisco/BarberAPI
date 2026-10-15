# Sistema Barbearia

API de agendamento para barbearias. Gerencia clientes, barbeiros, serviços e
agendamentos, com controle de pagamento e consulta de contas a receber (o
"fiado" da barbearia).

## Sobre o projeto

Este repositório contém o back-end do Sistema Barbearia: uma API REST que
centraliza os dados da barbearia e é consumida por um aplicativo mobile
desenvolvido em Flutter (em construção, em repositório separado). O projeto é
um **protótipo**, criado para demonstrar e validar a utilidade da solução com
barbearias locais.

O uso é interno: apenas o dono e os funcionários da barbearia operam o
sistema. Não há contas de cliente final nem login individual. O acesso à API é
protegido por uma chave compartilhada com o aplicativo.

## Funcionalidades

- Cadastro, edição e consulta de clientes, com vários telefones por cliente
- Cadastro de barbeiros e de serviços (preço e duração)
- Agendamento com cliente, barbeiro, data/hora e um ou mais serviços
- Valor total do agendamento calculado a partir dos serviços escolhidos
- Status do atendimento e status do pagamento controlados separadamente
- Consulta de contas a receber (atendimentos concluídos e ainda não pagos)
- Inativação em vez de exclusão para clientes, barbeiros e serviços, com
  possibilidade de reativar
- Proteção de todos os endpoints por API Key
- Documentação interativa dos endpoints via Swagger

## Tecnologias utilizadas

- **C#** com **.NET 10** e **ASP.NET Core Web API**
- **Entity Framework Core** com o provedor **Npgsql**
- **Supabase** (PostgreSQL gerenciado)
- **Swashbuckle** (Swagger / OpenAPI)
- **Docker** para empacotamento e **Render** para hospedagem
- **Clean Architecture** na organização do código

## Objetivo

Oferecer uma base simples e bem organizada para um sistema de agendamento de
barbearia, capaz de ser demonstrada a donos de barbearia e de evoluir para um
produto completo, sem precisar reescrever a estrutura.

## Link da API

*https://barberapi-q1yz.onrender.com/swagger/index.html*

> A API está no plano gratuito do Render, que coloca o serviço para dormir após
> alguns minutos sem uso. A primeira chamada depois de um período parado pode
> levar de 30 a 50 segundos. Antes de uma demonstração, abra o Swagger alguns
> minutos antes para "acordar" a API.

---

## Arquitetura

O projeto segue a Clean Architecture, dividido em cinco projetos:

```text
Barbearia/
├── Barbearia.API          Controllers, Middlewares, Program.cs, appsettings
├── Barbearia.Application  DTOs, Interfaces dos serviços, Services
├── Barbearia.Domain       Entidades, Enums, Interfaces dos repositórios
├── Barbearia.Infra.Data   AppDbContext, Configurations (EF), Repositories
└── Barbearia.Infra.Ioc    Registro das dependências (DependencyInjection.cs)
```

| Projeto | Responsabilidade |
|---|---|
| **API** | Receber as requisições HTTP, devolver as respostas e aplicar a API Key. Não tem regra de negócio nem acesso direto ao banco. |
| **Application** | Casos de uso: valida dados, monta as entidades, calcula o valor total e converte entidades em DTOs. |
| **Domain** | Núcleo do sistema: entidades, enums e contratos (interfaces) dos repositórios. Não depende de nenhuma tecnologia externa. |
| **Infra.Data** | Única camada que conhece o Entity Framework e o PostgreSQL: DbContext, mapeamento das tabelas e implementação dos repositórios. |
| **Infra.Ioc** | Centraliza a injeção de dependência e a configuração do banco, expostas no método `AddInfrastructure`. |

### Fluxo de uma requisição

```text
App / Swagger
      ↓
HTTP Request (header X-Api-Key)
      ↓
ApiKeyMiddleware
      ↓
Controller
      ↓
Service (Application)
      ↓
Interface do repositório (Domain)
      ↓
Repository (Infra.Data) → Entity Framework Core
      ↓
PostgreSQL (Supabase)
```

A resposta faz o caminho inverso, e o Controller devolve **DTOs**, nunca as
entidades do banco.

### Dependências entre os projetos

```text
API
 ├── Application
 └── Infra.Ioc

Application
 └── Domain

Infra.Data
 └── Domain

Infra.Ioc
 ├── Application
 ├── Domain
 └── Infra.Data

Domain
 └── não depende de nenhuma camada externa
```

## Modelo de dados

```text
Cliente  1 ──── N  Telefone
Cliente  1 ──── N  Agenda
Barbeiro 1 ──── N  Agenda
Agenda   N ──── N  Servico   (pela tabela Agenda_Servico)
```

| Tabela | Colunas principais |
|---|---|
| `cliente` | `id_cli`, `nome`, `status`, `observacao` |
| `telefone` | `id_tel`, `numero_tel`, `id_cli_fk` |
| `barbeiro` | `id_bar`, `nome`, `status`, `numero_tel` |
| `servico` | `id_ser`, `nome`, `preco` (`numeric(10,2)`), `tempo` (minutos), `observacao`, `status` |
| `agenda` | `id_age`, `data_hora_marcada`, `status_age`, `status_pag`, `id_cli_fk`, `id_bar_fk` |
| `agenda_servico` | `id_age_fk`, `id_ser_fk` (chave primária composta) |

Na tabela `agenda_servico`, apagar uma agenda remove os vínculos
automaticamente (`ON DELETE CASCADE`). Já apagar um serviço que está vinculado a
alguma agenda é bloqueado pelo banco (`ON DELETE RESTRICT`).

### Valores de status

| Campo | Valores aceitos |
|---|---|
| `status` (cliente, barbeiro, serviço) | `ativo`, `inativo` |
| `status_age` (agenda) | `agendado`, `confirmado`, `concluido`, `cancelado` |
| `status_pag` (agenda) | `pago`, `pendente` |

Os status são guardados como texto simples (`varchar`) e validados pela API.

## Regras de negócio

- **Inativar em vez de excluir:** `DELETE` em cliente, barbeiro e serviço apenas
  muda o status para `inativo`, preservando o histórico de agendamentos. Para
  voltar a usar o registro, existe o endpoint `PATCH /{id}/ativar`.
- **Exclusão definitiva:** só agendas (e seus vínculos com serviços) são
  apagadas de verdade.
- **Status fora da edição comum:** o `PUT` altera apenas os dados cadastrais.
  Status é alterado por endpoints próprios, para que ninguém reative um
  cadastro ou marque um pagamento sem querer durante uma edição.
- **Nascimento dos registros:** clientes, barbeiros e serviços nascem
  `ativo`. Agendas nascem `agendado` com pagamento `pendente`.
- **Valor total da agenda:** soma dos preços dos serviços vinculados, calculada
  na leitura. Se o preço de um serviço for alterado, o total de agendas antigas
  também muda.
- **Contas a receber:** agendas com `status_age = concluido` e
  `status_pag = pendente`. Agendas futuras e canceladas não entram.
- **Validações:** nome é obrigatório em cliente, barbeiro e serviço; o preço do
  serviço deve ser maior que zero; toda agenda precisa de ao menos um serviço.

## Referência da API

Todas as rotas começam com `/api` e exigem o header `X-Api-Key`.

Convenções de resposta: `201` na criação (com o header `Location`), `204` em
edições, inativações, reativações e mudanças de status, `400` com a mensagem de
validação, `404` quando o registro não existe e `401` sem a API Key.

### Cliente (`/api/Cliente`)

| Método | Rota | Descrição |
|---|---|---|
| GET | `/` | Lista os clientes com seus telefones |
| GET | `/{id}` | Busca um cliente |
| POST | `/` | Cria um cliente |
| PUT | `/{id}` | Edita nome e observação |
| DELETE | `/{id}` | Inativa o cliente |
| PATCH | `/{id}/ativar` | Reativa o cliente |
| GET | `/{id}/telefones` | Lista os telefones do cliente |
| POST | `/{id}/telefones` | Adiciona um telefone |
| PUT | `/{id}/telefones/{telefoneId}` | Altera um telefone |
| DELETE | `/{id}/telefones/{telefoneId}` | Remove um telefone (exclusão definitiva) |

### Barbeiro (`/api/Barbeiro`)

| Método | Rota | Descrição |
|---|---|---|
| GET | `/` | Lista os barbeiros |
| GET | `/{id}` | Busca um barbeiro |
| POST | `/` | Cria um barbeiro |
| PUT | `/{id}` | Edita nome e telefone |
| DELETE | `/{id}` | Inativa o barbeiro |
| PATCH | `/{id}/ativar` | Reativa o barbeiro |

### Serviço (`/api/Servico`)

| Método | Rota | Descrição |
|---|---|---|
| GET | `/` | Lista os serviços |
| GET | `/{id}` | Busca um serviço |
| POST | `/` | Cria um serviço |
| PUT | `/{id}` | Edita nome, preço, observação e tempo |
| DELETE | `/{id}` | Inativa o serviço |
| PATCH | `/{id}/ativar` | Reativa o serviço |

### Agenda (`/api/Agenda`)

| Método | Rota | Descrição |
|---|---|---|
| GET | `/` | Lista as agendas com cliente, barbeiro, serviços e valor total |
| GET | `/{id}` | Busca uma agenda |
| GET | `/contas-receber` | Lista as agendas concluídas e ainda não pagas |
| POST | `/` | Cria uma agenda com seus serviços |
| PUT | `/{id}` | Altera data/hora e a lista de serviços |
| DELETE | `/{id}` | Exclui a agenda definitivamente |
| PATCH | `/{id}/status-agendamento` | Muda o status do atendimento |
| PATCH | `/{id}/status-pagamento` | Muda o status do pagamento |

## Acesso e segurança

Todas as rotas, exceto o Swagger, exigem o header `X-Api-Key` com a chave
configurada na API. Sem o header, a resposta é `401` ("API Key ausente."); com
um valor errado, `401` ("API Key inválida.").

Como usar no Swagger: clique em **Authorize**, cole a chave no campo **Value**
(somente o valor) e confirme.

A chave fica no aplicativo e na API e precisa ser a mesma nos dois lados. Ela
não tem relação com o banco de dados. Por estar embutida no app, ela impede
acesso casual, mas não impede um atacante determinado; para um sistema final,
o caminho é login individual com JWT.

### Segredos

Nada sensível é versionado. A senha do banco e a API Key ficam:

- **Localmente:** em `appsettings.Development.json`, que está no `.gitignore`.
- **Em produção:** em variáveis de ambiente no Render.

O `appsettings.json` versionado contém apenas valores vazios.

## Desenvolvimento

Requisitos: **.NET 10 SDK** e acesso a um banco PostgreSQL (Supabase).

Crie o arquivo `Barbearia.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=SEU_HOST;Port=5432;Database=postgres;Username=SEU_USUARIO;Password=SUA_SENHA;SSL Mode=Require;Trust Server Certificate=true"
  },
  "ApiKey": "UM_GUID_GERADO_POR_VOCE"
}
```

Para gerar uma chave, no PowerShell: `[guid]::NewGuid()`.

```bash
dotnet build                          # compila a solução
dotnet run --project Barbearia.API    # sobe a API localmente
```

O Swagger fica disponível em `/swagger` (apenas no ambiente `Development`).

### Variáveis de ambiente (produção)

```
ConnectionStrings__DefaultConnection
ApiKey
ASPNETCORE_ENVIRONMENT
```

Os dois underscores em `ConnectionStrings__DefaultConnection` são como o .NET
lê o aninhamento do JSON. A variável `ASPNETCORE_ENVIRONMENT=Development` só é
necessária para manter o Swagger no ar, já que ele é ligado apenas nesse
ambiente.

## Hospedagem

A API roda no **Render**, usando o `Dockerfile` do projeto
(`Barbearia.API/Dockerfile`, com a raiz do repositório como contexto de build).
O deploy é automático a cada push na branch principal. Se o Render reclamar de
porta, defina `PORT=8080`.

## Decisões de projeto

- **DTOs em vez de entidades nas respostas:** evita ciclos de referência na
  serialização (Cliente ↔ Telefone) e impede que campos internos vazem pela API.
- **Status como texto, sem enum nativo do Postgres:** o mapeamento de enums
  nativos com o EF Core e o Npgsql se mostrou instável (erros na leitura e na
  gravação). Trocar as colunas para `varchar` e validar na API deu um
  comportamento previsível, e os valores já vêm de opções fixas no aplicativo.
- **Inativação (soft delete):** preserva o histórico de agendamentos e evita
  conflito com as chaves estrangeiras.
- **Tabela `agenda_servico`:** uma agenda pode ter vários serviços, então a
  relação é muitos-para-muitos, com chave composta.
- **API Key em vez de JWT:** por ser um protótipo de uso interno, a proteção
  simples foi suficiente e evitou complexidade desnecessária nesta fase.
