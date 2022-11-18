use videoteca

CREATE TABLE [dbo].[Genero]
(
	[Id] INT NOT NULL IDENTITY, 
    [des_genero] VARCHAR(50) NOT NULL, 
	[dtc_atualizacao] DATETIME DEFAULT GETDATE()
)

ALTER TABLE [dbo].[Genero]
ADD CONSTRAINT idxGeneroPK PRIMARY KEY (Id)

