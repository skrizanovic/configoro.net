CREATE TABLE [dbo].[ConfigurationSetting] (
    [ConfigurationSettingId]  INT           IDENTITY (1, 1) NOT NULL,
    [ConfigurationTemplateId] INT           NOT NULL,
    [ProcessorTypeId]         INT           NOT NULL,
    [XpathValue]              VARCHAR (500) NOT NULL,
    [ChangePropertyName]      VARCHAR (200) NULL,
    CONSTRAINT [PK_ConfigurationSetting] PRIMARY KEY CLUSTERED ([ConfigurationSettingId] ASC),
    CONSTRAINT [FK_ConfigurationSetting_ConfigurationTemplate] FOREIGN KEY ([ConfigurationTemplateId]) REFERENCES [dbo].[ConfigurationTemplate] ([ConfigurationTemplateId]),
    CONSTRAINT [FK_ConfigurationSetting_ProcessorType] FOREIGN KEY ([ProcessorTypeId]) REFERENCES [dbo].[ProcessorType] ([ProcessorTypeId])
);

