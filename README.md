# Finance Manager API 📊

O **Finance Manager** é uma aplicação para gerenciamento financeiro pessoal. Ela permite que usuários acompanhem receitas, despesas e saldos, com suporte para categorização e relatórios financeiros. A API foi desenvolvida para ser eficiente, extensível e de fácil manutenção, utilizando tecnologias modernas e práticas recomendadas.

---

## 🧠 **Visão Geral**

Este projeto foi desenvolvido com o objetivo de **aprimorar habilidades de desenvolvimento** em tecnologias modernas, além de explorar boas práticas na criação de APIs e integração com bancos de dados. Embora seja funcional como um sistema de controle financeiro, sua principal finalidade é educacional. O foco do projeto está em:

- 📈 **Organização e Estrutura**: Implementar um sistema de gerenciamento financeiro com práticas de design escaláveis.
- ⚡ **Aprendizado Técnico**: Usar tecnologias como **Dapper**, **.NET 8**, **Testes Unitários**, **PostgreSQL**, **Docker** e rotinas **Deploy** para reforçar conceitos fundamentais de programação e banco de dados.
- 🌐 **Flexibilidade e Integração**: Desenvolver uma API que pode operar localmente e ser integrada a serviços baseados em nuvem, com suporte para ambientes containerizados como **Docker**.

---

## 🛠 **Tecnologias Utilizadas**

### 🖥 Backend
- **.NET 8**: Framework maduro e eficiente para desenvolvimento de APIs RESTful, com suporte a alta performance e extensibilidade.
- **Dapper**: Biblioteca leve para mapeamento objeto-relacional (ORM) que oferece alto desempenho e simplicidade no acesso ao banco de dados.

### 🗄 Banco de Dados
- **PostgreSQL**: Um banco de dados relacional robusto e open-source, ideal para gerenciamento transacional, suporte a JSON e flexibilidade para crescimento.

### 🔗 Outras Tecnologias
- **Docker**: Facilita o deploy e gerenciamento do ambiente de desenvolvimento e produção.
- **Npgsql**: Biblioteca .NET para comunicação com o PostgreSQL.

---

## ✨ **Por que essas tecnologias?**

- **.NET 8**:
  - Framework consolidado, com ferramentas nativas para desenvolvimento de APIs RESTful.
  - Suporte ao **hot reload**, facilitando o desenvolvimento local.
  - Comunidade ativa e ferramentas maduras.

- **Dapper**:
  - Simplicidade e desempenho superior para acesso direto ao banco.
  - Ideal para projetos onde não é necessário o overhead de ORMs como Entity Framework.

- **PostgreSQL**:
  - Custo-benefício: Open-source e amplamente suportado em provedores de nuvem.
  - Suporte a **JSON** e transações complexas, ideal para dados estruturados e semi-estruturados.
  - Excelente para aplicações financeiras devido à precisão e consistência.

- **Docker**:
  - Permite isolar o ambiente de desenvolvimento, garantindo consistência entre local e produção.
  - Reduz problemas de configuração e dependências.

---

## 🚀 **Como Rodar o Projeto Localmente**

### Pré-requisitos
1. **Instalar Dependências**:
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - [Docker](https://www.docker.com/get-started)
   - [PostgreSQL (opcional, caso prefira rodar sem Docker)](https://www.postgresql.org/download/)

2. **Ferramentas Opcionais**:
   - **Visual Studio 2022** ou **Visual Studio Code** para edição e debug.
   - **Postman** ou **cURL** para testar os endpoints da API.

---

### ⚙️ Passos para Configuração

1. **Clone o Repositório**
   ```bash
   git clone https://github.com/seu-usuario/finance-manager-api.git
   cd finance-manager-api
   ```

2. **Configure o Banco de Dados (Docker)**
   - Suba os containers:
     ```bash
     docker-compose up --build
     ```

3. **Configure o Banco de Dados (Sem Docker - opcional)**
   - Configure uma instância do PostgreSQL local e atualize a string de conexão no `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=finance_manager_db;Username=postgres;Password=password"
     }
     ```

4. **Instale as Dependências**
   ```bash
   dotnet restore
   ```

5. **Execute o Projeto**
   ```bash
   dotnet run
   ```

6. **Teste os Endpoints**
   - Acesse a API em `http://localhost:5000`.
   - Teste os endpoints com **Postman** ou diretamente no navegador.

---

## 📚 **Funcionalidades Disponíveis**

- **Usuários**:
  - Criar, listar e gerenciar contas de usuário.
- **Contas**:
  - Criar e gerenciar contas financeiras.
- **Transações**:
  - Registrar receitas e despesas, organizadas por categorias.
