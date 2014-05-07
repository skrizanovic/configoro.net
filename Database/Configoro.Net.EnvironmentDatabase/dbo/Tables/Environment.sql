CREATE TABLE [dbo].[Environment] (
    [EnvironmentId]   INT           IDENTITY (1, 1) NOT NULL,
    [EnvironmentName] VARCHAR (100) NULL,
    [IsActive]        BIT           NOT NULL,
    CONSTRAINT [PK_Environmet] PRIMARY KEY CLUSTERED ([EnvironmentId] ASC)
);

