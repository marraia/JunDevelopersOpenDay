CREATE TABLE [dbo].[Palestrantes] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [Nome]           VARCHAR (50) NOT NULL,
    [Empresa]        VARCHAR (50) NOT NULL,
    [TituloPalestra] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Palestrantes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

