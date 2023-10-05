


## Overview

**Description:**
The QueueHangFire API receives a notification from a main server containing the ID of a previously saved .Csv file in the database. It processes and saves the file in the main application database.

The Hangfire library is being used to create a job queue for processing .Csv files.

## Before You Begin

**Dependencies Installation:**
Before running the application, make sure to install the project's dependencies. Environment variables are currently hard-coded in the code. In a production scenario, they should be replaced with an .env file. Please note that this project is still in development and testing, and is not yet suitable for production use.

To install the project dependencies, you can run the following command:

```
dotnet restore
```

## Getting Started

After installing the dependencies, navigate to the project's root directory, where the Program.cs file is located, in a terminal, and run the following command:

```
dotnet watch run
```

If everything runs correctly, you can access the Swagger UI interface at the following address:

[Swagger UI](http://localhost:5000/index.html)

<hr>

<h1 id="queuehangfire-portuguese">QueueHangFire (Português)</h1>

## Visão Geral

**Descrição:**
A API QueueHangFire recebe uma notificação de um servidor principal contendo o ID de um arquivo .Csv previamente salvo no banco de dados. Ela processa e salva o arquivo no banco de dados principal da aplicação.

A biblioteca Hangfire está sendo usada para criar uma fila de trabalhos (Jobs) para processar arquivos .Csv.

## Antes de Começar

**Instalação de Dependências:**
Antes de executar a aplicação, certifique-se de instalar as dependências do projeto. As variáveis de ambiente estão atualmente codificadas no código. Em um cenário de produção, elas devem ser substituídas por um arquivo .env. Por favor, note que este projeto ainda está em desenvolvimento e teste e ainda não é adequado para uso em produção.

Para instalar as dependências do projeto, você pode executar o seguinte comando:

```
dotnet restore
```

## Iniciando

Após a instalação das dependências, navegue até o diretório raiz do projeto, onde o arquivo Program.cs está localizado, em um terminal, e execute o seguinte comando:

```
dotnet watch run
```

Se tudo correr corretamente, você pode acessar a interface Swagger UI no seguinte endereço:

[Swagger UI](http://localhost:5000/index.html)
```
