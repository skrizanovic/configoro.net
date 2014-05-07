CREATE TABLE [dbo].[ConfigurationSettingValue] (
    [ConfigurationSettingValueID] INT IDENTITY (1, 1) NOT NULL,
    [ConfigurationSettingId]      INT NOT NULL,
    [ConfigValueId]               INT NOT NULL,
    CONSTRAINT [PK_ConfigurationSettingValues] PRIMARY KEY CLUSTERED ([ConfigurationSettingValueID] ASC),
    CONSTRAINT [FK_ConfigurationSettingValues_ConfigurationSetting] FOREIGN KEY ([ConfigurationSettingId]) REFERENCES [dbo].[ConfigurationSetting] ([ConfigurationSettingId]),
    CONSTRAINT [FK_ConfigurationSettingValues_ConfigValue] FOREIGN KEY ([ConfigValueId]) REFERENCES [dbo].[ConfigValue] ([ConfigValueId])
);

