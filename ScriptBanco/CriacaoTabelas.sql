--tabela authors
CREATE TABLE authors (
  id INT NOT NULL PRIMARY KEY,
  first_name VARCHAR(50) not null, 
  last_name VARCHAR(50) not null, 
  email VARCHAR(100) not null, 
  birthdate DATETIME not null, 
  added VARCHAR(20) not null, 
  CONSTRAINT email_unique UNIQUE (email)
) ;

--tabela posts
CREATE TABLE posts(
  id INT NOT NULL PRIMARY KEY,
 author_id int  not null  FOREIGN KEY REFERENCES authors(id), 
 title VARCHAR(255)  not null, 
 descriptionpost VARCHAR(500)  not null, 
 contentspost VARCHAR(2500)  not null, 
 datepost DATETIME  not null );
 
 --tabela rates
 CREATE TABLE rates(
  id INT NOT NULL PRIMARY KEY,
 author_id int  not null  FOREIGN KEY REFERENCES authors(id), 
 post_id int  not null  FOREIGN KEY REFERENCES posts(id), 
 daterate  DATETIME  not null	,
 noterate VARCHAR(500)  not null
  );