# 📦 Inventory System

Sistema de inventário distribuído em microsserviços, implementado em .NET com API Gateway via Ocelot e orquestração de contêineres com Docker Compose.

---

## 🚀 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

---

## 🧱 Estrutura do Projeto

```plaintext
InventorySystem/
│
├── docker-compose.yml               # Orquestração de todos os serviços
│
├── UserService/                     # Serviço de usuários
│   ├── model/User.cs
│   ├── data/UserDbContext.cs
│   ├── service/UserServices.cs
│   ├── controller/UserController.cs
│   └── Dockerfile
│
├── ProductService/                  # Serviço de produtos
│   ├── model/Product.cs
│   ├── data/ProductDbContext.cs
│   ├── service/ProductServices.cs
│   ├── controller/ProductController.cs
│   └── Dockerfile
│
├── OperationService/               # Serviço de operações (entradas/saídas)
│   ├── model/Operation.cs
│   ├── data/OperationDbContext.cs
│   ├── service/OperationServices.cs
│   ├── controller/OperationController.cs
│   └── Dockerfile
│
├── StockService/                   # Serviço de controle de estoque
│   ├── model/Stock.cs
│   ├── data/StockDbContext.cs
│   ├── service/StockServices.cs
│   ├── controller/StockController.cs
│   └── Dockerfile
│
└── ApiGateway/                     # Gateway central usando Ocelot
    ├── program.cs
    ├── appsettings.json
    ├── ocelot.json
    └── Dockerfile
```

---

## 🐳 Como Rodar o Projeto

### Pré-requisitos

- Docker
- Docker Compose

### Instruções

1. Clone o repositório:

```bash
git clone [https://github.com/seu-usuario/inventory-system.git](https://github.com/Jjunior112/inventorySystemMicroservice)
cd inventorySystem
```

2. Suba os contêineres:

```bash
docker-compose up --build
```

3. Acesse o API Gateway em:

```
http://localhost:8000
```

---

## 🔁 Serviços Disponíveis

| Serviço           | Porta Padrão | Descrição                         |
|-------------------|--------------|-----------------------------------|
| UserService       | 5001         | Gerenciamento de usuários         |
| ProductService    | 5002         | Cadastro e listagem de produtos  |
| OperationService  | 5003         | Operações de entrada/saída       |
| StockService      | 5004         | Controle de estoque              |
| API Gateway       | 9000         | Encaminhamento de requisições    |

> Todos os endpoints devem ser acessados via Gateway (`localhost:9000`) após configuração do `ocelot.json`.
> As portas padrões podem ser alteradas no arquivo .env
---

## 📌 Próximos Passos

- Integração com AuthService (JWT)
- Documentação com Swagger
- Serviço de reserva
- Serviço de confirmação de reserva
---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
