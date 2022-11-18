CREATE TABLE [dbo].[Cliente]
(
	[Id] INT NOT NULL IDENTITY, 
    [num_documento] VARCHAR(14) NOT NULL, 
    [nom_cliente] VARCHAR(50) NOT NULL, 
    [cod_tipo_pessoa] TINYINT NOT NULL, 
    [des_email] VARCHAR(100) NULL, 
    [num_telefone] VARCHAR(20) NULL, 
    [num_cep] VARCHAR(8) NULL, 
    [des_municipio] VARCHAR(50) NULL, 
    [des_bairro] VARCHAR(50) NULL, 
    [des_logradouro] VARCHAR(100) NULL, 
    [num_endereco] VARCHAR(10) NULL, 
    [des_complemento] VARCHAR(50) NULL,
    [dtc_nascimento] DATETIME NULL,
    [dtc_atualizacao] DATETIME DEFAULT GETDATE()
)

ALTER TABLE [dbo].[Cliente]
ADD CONSTRAINT idxClientePK PRIMARY KEY (Id)

CREATE INDEX idxCliente001
ON [dbo].[Cliente] (nom_cliente) 

CREATE INDEX idxCliente002
ON [dbo].[Cliente] (num_documento) 
