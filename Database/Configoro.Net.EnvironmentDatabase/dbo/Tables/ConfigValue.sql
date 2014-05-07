CREATE TABLE [dbo].[ConfigValue] (
    [ConfigValueId] INT           IDENTITY (1, 1) NOT NULL,
    [EnvironmentId] INT           NOT NULL,
    [Name]          VARCHAR (50)  NOT NULL,
    [Value]         VARCHAR (500) NOT NULL,
    [IsActive]      BIT           NOT NULL,
    CONSTRAINT [PK_ConfigValue] PRIMARY KEY CLUSTERED ([ConfigValueId] ASC),
    CONSTRAINT [FK_ConfigValue_Environmet] FOREIGN KEY ([EnvironmentId]) REFERENCES [dbo].[Environment] ([EnvironmentId])
);

