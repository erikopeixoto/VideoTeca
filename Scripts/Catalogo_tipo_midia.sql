USE VIDEOTECA

CREATE TABLE [dbo].[Catalogo_tipo_midia]
(
	[Id] INT NOT NULL IDENTITY, 
    [id_catalogo] INT NOT NULL, 
    [id_tipo_midia] INT NOT NULL, 
    [qtd_titulo] INT NULL, 
    [qtd_disponivel] INT NULL, 
	[dtc_atualizacao] DATETIME DEFAULT GETDATE()
)

ALTER TABLE [dbo].[Catalogo_tipo_midia]
ADD CONSTRAINT idxCatalogoTipoMidiaPK PRIMARY KEY (Id)

CREATE INDEX idxCatalogoTipoMidia001
ON [dbo].[Catalogo_tipo_midia] (id_catalogo) 

CREATE INDEX idxCatalogoTipoMidia002
ON [dbo].[Catalogo_tipo_midia] (id_tipo_midia) 
