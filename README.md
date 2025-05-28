# 📦 Inventory System

Sistema de inventário distribuído em microsserviços, implementado em .NET com API Gateway via Ocelot, orquestração de contêineres com Docker Compose, sistema de cache usando Redis e comunicação entre microsserviços usando RabbitMq.

---

## 🚀 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Ocelot API Gateway](https://ocelot.readthedocs.io/)
- [RabbitMq](https://www.rabbitmq.com/)
- [Redis](https://redis.io/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

---

## 🧱 Estrutura do Projeto

```plaintext
InventorySystem/
│
├── docker-compose.yml               # Orquestração de todos os serviços, gateway e frontend
│
├── Contracts/                        # Camada de frontend da aplicação
│        ├── Caching
│        ├── Enums
│        ├── Events
│        └── Responses
├── Frontend/                        # Camada de frontend da aplicação
│  
├── Gateway/ 
│    └── ApiGateway/                     # Gateway central usando Ocelot
│        ├── program.cs
│        ├── appsettings.json
│        ├── ocelot.json
│        └── Dockerfile
├── Services/ 
│    ├── ProductService/                  # Serviço de produtos
│       ├── model/Product.cs
│       ├── data/ProductDbContext.cs
│       ├── caching/
│       	├── RedisCachingService.cs
│       ├── service/ProductServices.cs
│       ├── controller/ProductController.cs
│       └── Dockerfile
│    
│    ├── OperationService/               # Serviço de operações (entradas/saídas)
│       ├── model/Operation.cs
│       ├── data/OperationDbContext.cs
│       ├── caching/
│       	├── RedisCachingService.cs
│       ├── service/OperationServices.cs
│       ├── controller/OperationController.cs
│       └── Dockerfile
│    
│    ├── StockService/                   # Serviço de controle de estoque
│       ├── consumers/
│       	├── OperationCreatedConsumer.cs
│       	├── ProductCreatedConsumer.cs
│       ├── model/Stock.cs
│       ├── data/StockDbContext.cs
│       ├── caching/
│       	├── RedisCachingService.cs
│       ├── service/StockServices.cs
│       ├── controller/StockController.cs
│       └── Dockerfile
└──   

```

---

## 🐳 Como Rodar o Projeto

### Pré-requisitos

- Docker
- Docker Compose

### Instruções

1. Clone o repositório:

```bash
git clone https://github.com/Jjunior112/inventorySystemMicroservice
cd inventorySystem
```

2. Suba os contêineres:

```bash
docker-compose up --build
```

3. Acesse o API Gateway em:

```
http://localhost:9000
```

---

## 🔁 Serviços Disponíveis

| Serviço           | Porta Padrão | Descrição                        |
|-------------------|--------------|----------------------------------|
| Frontend          | 3000         | Frontend da aplicação            |
| ProductService    | 5002         | Cadastro e listagem de produtos  |
| OperationService  | 5003         | Operações de entrada/saída       |
| StockService      | 5004         | Controle de estoque              |
| API Gateway       | 9000         | Encaminhamento de requisições    |


> Todos os endpoints devem ser acessados via Gateway (`localhost:9000`) após configuração do `ocelot.json`.
> As portas padrões podem ser alteradas no arquivo .env
---
# Variáveis de Ambiente (.env)

Certifique-se de ter as seguintes variáveis no seu arquivo `.env` na raiz do projeto:

```env
RABBITMQ_DEFAULT_USER=guest
RABBITMQ_DEFAULT_PASS=guest

SA_PASSWORD_USER=Seu@123
SA_PASSWORD_PRODUCT=Seu@123
SA_PASSWORD_OPERATIONS=Seu@123
SA_PASSWORD_STOCK=Seu@123

USER_DB_PORT=1433
PRODUCT_DB_PORT=1434
OPERATIONS_DB_PORT=1435
STOCK_DB_PORT=1436

USER_SERVICE_PORT=5001
PRODUCT_SERVICE_PORT=5002
OPERATIONS_SERVICE_PORT=5003
STOCK_SERVICE_PORT=5004
API_GATEWAY_PORT=9000
FRONTEND_PORT = 3000
```
##  Rotas

### Produtos

- **GET /products**
  - Retorna a lista de todos os produtos.
  - **Exemplo de resposta:**
    ```json
    [
      {
        "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
        "productName": "Sample Product",
        "productCategory": "Sample Category",
        "createdAt": "2025-04-19T03:34:14.0829381+00:00"
      }
    ]
    ```

- **GET /products/{id}**
  - Retorna um produto específico com base no ID.
  - **Exemplo de resposta:**
    ```json
    {
      "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
      "productName": "Sample Product",
      "productCategory": "Sample Category",
      "createdAt": "2025-04-19T03:34:14.0829381+00:00"
    }
    ```

- **POST /products**
  - Cria um novo produto.
  - **Exemplo de corpo de requisição:**
    ```json
    {
      "productName": "Sample Product",
      "productCategory": "Sample Category"
    }
    ```

### Operações

- **GET /operations**
  - Retorna a lista de todas as operações realizadas.
  - **Exemplo de resposta:**
    ```json
    [
      {
        "operationId": "d4f07a7a-40fd-46f0-9cba-912f9b98a93e",
        "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
        "productName":"Sample Product",
        "operationQuantity": 150,
        "operationType": StockIn,
        "operationAt": "2025-04-19T03:34:25.9385576+00:00"
      }
    ]
    ```

- **GET /operations/{id}**
  - Retorna uma operação específica com base no ID.
  - **Exemplo de resposta:**
    ```json
    {
      "operationId": "d4f07a7a-40fd-46f0-9cba-912f9b98a93e",
      "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
      "productName":"Sample Product",
      "operationQuantity": 150,
      "operationType": StockIn,
      "operationAt": "2025-04-19T03:34:25.9385576+00:00"
    }
    

### Estoques

- **GET /stocks**
  - Retorna a lista de todos os estoques.
  - **Exemplo de resposta:**
    ```json
    [
      {
       "stockId": "e146af0e-ad59-44d5-93fa-c3ceaff650f0",
        "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
        "productName": "Sample Product",
        "productCategory": "Sample Category",
        "createdAt": "2025-04-19T03:34:14.0829381",
        "productQuantity": 150
      }
    ]
    ```

- **GET /stocks/{id}**
  - Retorna um estoque específico com base no ID.
  - **Exemplo de resposta:**
    ```json
    {
         "stockId": "e146af0e-ad59-44d5-93fa-c3ceaff650f0",
        "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
        "productName": "Sample Product",
        "productCategory": "Sample Category",
        "createdAt": "2025-04-19T03:34:14.0829381",
        "productQuantity": 150
    }
    ```
    ```

- **POST /stocks**
  - atualiza o estoque e cria uma nova operação.
  - **Exemplo de corpo de requisição:**
    ```json
    {
      "productId": "acc51146-8873-4cff-ac5f-ab854a75c13b",
      "productName":"Sample Product",
      "operationQuantity": 150,
      "operationType": StockIn
    }
    ```

## 📨 Comunicação Assíncrona com RabbitMQ

Este sistema utiliza o RabbitMQ para orquestrar eventos entre microsserviços. O MassTransit é usado como biblioteca de mensageria para publicar e consumir eventos.

### Eventos Publicados:
- `ProductCreatedEvent` pelo `ProductService`
- `OperationCreatedEvent` no `StockService`

### Eventos Consumidos:
- `ProductCreatedEvent` no `StockService`
- `OperationCreatedEvent` pelo `OperationService`

RabbitMQ está configurado no `docker-compose.yml` e pode ser acessado em `http://localhost:15672` com login `guest` / `guest`.

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
