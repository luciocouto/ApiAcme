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


Para rodar a apica��o basta criar no SqlServer uma database com um usuario, alterar a conex�o no appsettings.json e rodar o script de cria��o de tabelas e depois rodar a aplica��o.
Ao iniciar a aplica��o ela ir� verificar se a tabela de Authors tem algum registro, sen�o ela vai popular as tabelas com alguns registros.