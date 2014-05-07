CREATE TABLE [dbo].[ConfigurationTemplate] (
    [ConfigurationTemplateId] INT           IDENTITY (1, 1) NOT NULL,
    [TemplateName]            VARCHAR (200) NOT NULL,
    [ConfigFile]              VARCHAR (200) NULL,
    [ConfigType]              INT           NULL,
    [IsActive]                BIT           NOT NULL,
    CONSTRAINT [PK_ConfigurationTemplate] PRIMARY KEY CLUSTERED ([ConfigurationTemplateId] ASC)
);

