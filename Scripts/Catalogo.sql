USE VIDEOTECA

CREATE TABLE [dbo].[Catalogo]
(
	[Id] INT NOT NULL IDENTITY, 
    [cod_catalogo] VARCHAR(5) NOT NULL, 
    [id_genero] INT NOT NULL, 
    [des_titulo] VARCHAR(100) NULL, 
    [nom_autor] VARCHAR(100) NULL, 
    [ano_lancamento] VARCHAR(4) NULL, 
	[dtc_atualizacao] DATETIME DEFAULT GETDATE()
)

ALTER TABLE [dbo].[Catalogo]
ADD CONSTRAINT idxCatalogoPK PRIMARY KEY (Id)

CREATE INDEX idxCatalogo001
ON [dbo].[Catalogo] (cod_catalogo) 

CREATE INDEX idxCatalogo002
ON [dbo].[Catalogo] (id_genero) 
