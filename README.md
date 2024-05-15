# Projeto Web API para Plataforma de Freelancers

## Descrição

Este projeto consiste em uma Web API desenvolvida em C# com o intuito de fornecer métodos para uma plataforma que conecta desenvolvedores freelancers a projetos de clientes. A API facilita a busca, cadastro e gerenciamento de freelancers e projetos, utilizando uma arquitetura limpa e diversas tecnologias modernas.

## Arquitetura

O projeto segue os princípios da arquitetura limpa, separando as responsabilidades em diferentes camadas:

- **API**: Camada responsável por expor os endpoints da Web API.
- **CORE**: Contém as entidades e interfaces principais do domínio.
- **Application**: Implementa os casos de uso e a lógica de aplicação, utilizando o padrão CQRS (Command Query Responsibility Segregation).
- **Infra**: Implementação da infraestrutura, como repositórios e comunicação com o banco de dados e microserviços.
- **Tests**: Projetos de testes unitários e de integração, garantindo a qualidade e o funcionamento correto da aplicação.

## Tecnologias Utilizadas

O projeto utiliza as seguintes tecnologias e padrões:

- **.NET 8**: Framework principal para o desenvolvimento da aplicação.
- **Padrão Repository**: Padrão que abstrai o acesso aos dados, centralizando as operações em um repositório.
- **MediatR**: Biblioteca para implementar o padrão Mediator, facilitando a comunicação entre componentes da aplicação.
- **xUnit**: Framework de testes unitários utilizado para garantir a qualidade do código.
- **CQRS (Command Query Responsibility Segregation)**: Padrão arquitetural que separa operações de leitura e escrita em diferentes modelos.
- **Entity Framework Core**: ORM (Object-Relational Mapper) utilizado para comunicação com o banco de dados.
- **FluentValidation**: Biblioteca para validação de objetos de forma fluente e desacoplada.
- **RabbitMQ**: Mensageria utilizada para implementar a arquitetura de microservices, permitindo a comunicação assíncrona entre diferentes partes da aplicação.

Este README fornece uma visão geral do projeto, tecnologias e arquitetura utilizados. Para mais detalhes sobre como configurar e utilizar o projeto, consulte a documentação específica de cada camada e os comentários no código fonte.
