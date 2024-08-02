# InvestmentAuthenticationAPI

Baixe a imagem docker do SQLServer e execute em um container docker

mcr.microsoft.com/mssql/server:2022-latest

Após executa-la em um container docker, acesso sua instancia através do Azure Data Studio ou SQL Management Studio e, exucute os scripts abaixo

CREATE TABLE [dbo].[User]
(
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    NAME VARCHAR(256) NOT NULL,
    USERNAME VARCHAR(70) NOT NULL,
    EMAIL VARCHAR(300) NOT NULL,
    PASSWORD VARCHAR(80) NOT NULL,
    CREATEDAT DATETIME
);

Authentication

No endpoint NewUser, informe as credencias de acesso, que devem ser cadastradas, conforme exemplo abaixo:

CREATE TABLE [dbo].[Portfolio]
(
    ID int IDENTITY(1,1) PRIMARY KEY,
    USER_ID int,
    TYPE_ID int,
    NAME VARCHAR(100) NOT NULL,
    DESCRIPTION VARCHAR(200) NOT NULL 
);

CREATE TABLE [dbo].[AssetType]
(
    ID int IDENTITY(1,1) PRIMARY KEY,
    NAME VARCHAR(30),
    DESCRIPTION VARCHAR(1000),
    NEGOCIATION_CODE VARCHAR(5)
);

CREATE TABLE [dbo].[Asset]
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TYPE_ID INT,
    NAME VARCHAR(200),
    SYMBOL_CODE VARCHAR(50),
    PRICE DECIMAL
);

CREATE TABLE [dbo].[Transaction]
(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    PORTFOLIO_ID INT,
    ASSET_ID INT,
    OPERATION_TYPE VARCHAR(10),
    QUANTITY INT,
    UNIT_PRICE DECIMAL,
    VALUE DECIMAL,
    TRANSACTION_DATE DATETIME,
);


ALTER TABLE [Transaction]
ADD CONSTRAINT FK_Portfolio1 FOREIGN KEY (PORTFOLIO_ID)
REFERENCES [Portfolio] (ID);

ALTER TABLE [Transaction]
ADD CONSTRAINT FK_Asset1 FOREIGN KEY (ASSET_ID)
REFERENCES [Asset] (ID);

--=============================
Drop table [dbo].[User]
--//User 
CREATE TABLE [dbo].[User] (
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(256),
    ClientId VARCHAR(256),
    Secret VARCHAR(256),
    CreatedAt DATETIME 
); 
