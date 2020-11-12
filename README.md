# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact


Para rodar a apicação basta criar no SqlServer uma database com um usuario, alterar a conexão no appsettings.json e rodar o script de criação de tabelas e depois rodar a aplicação.
Ao iniciar a aplicação ela irá verificar se a tabela de Authors tem algum registro, senão ela vai popular as tabelas com alguns registros.