use videoteca

CREATE TABLE [dbo].[Tipo_Midia]
(
	[Id] INT NOT NULL IDENTITY, 
    [des_tipo_midia] VARCHAR(50) NOT NULL, 
	[dtc_atualizacao] DATETIME DEFAULT GETDATE()
)

ALTER TABLE [dbo].[Tipo_Midia]
ADD CONSTRAINT idxTipoMidiaPK PRIMARY KEY (Id)

