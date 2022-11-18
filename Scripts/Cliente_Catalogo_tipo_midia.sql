USE VIDEOTECA

CREATE TABLE [dbo].[Cliente_catalogo_tipo_midia]
(
	[Id] INT NOT NULL IDENTITY, 
    [id_cliente] INT NOT NULL, 
    [id_catalogo_tipo_midia] INT NOT NULL, 
    [qtd_titulo] INT NULL, 
	[dtc_locacao] DATETIME,
	[dtc_entrega] DATETIME,
	[dtc_devolucao] DATETIME,
	[dtc_atualizacao] DATETIME DEFAULT GETDATE()
)

ALTER TABLE [dbo].[Cliente_catalogo_tipo_midia]
ADD CONSTRAINT idxClienteCatalTpMidiaPK PRIMARY KEY (Id)

CREATE INDEX idxClienteCatalTpMidia001
ON [dbo].[Cliente_catalogo_tipo_midia] (id_cliente) 

CREATE INDEX idxClienteCatalTpMidia002
ON [dbo].[Cliente_catalogo_tipo_midia] (id_catalogo_tipo_midia) 
