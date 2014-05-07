CREATE VIEW dbo.vw_ConfigPerEnvironment
AS
SELECT        dbo.Environment.EnvironmentName, dbo.ConfigValue.Name, dbo.ConfigurationSetting.XpathValue, dbo.ConfigurationSetting.ChangePropertyName, 
                         dbo.ConfigValue.Value, dbo.ConfigurationTemplate.TemplateName, dbo.ConfigurationSettingValue.ConfigurationSettingValueID, 
                         dbo.ConfigurationSetting.ProcessorTypeId
FROM            dbo.Environment INNER JOIN
                         dbo.ConfigValue ON dbo.Environment.EnvironmentId = dbo.ConfigValue.EnvironmentId CROSS JOIN
                         dbo.ConfigurationTemplate INNER JOIN
                         dbo.ConfigurationSetting ON dbo.ConfigurationTemplate.ConfigurationTemplateId = dbo.ConfigurationSetting.ConfigurationTemplateId INNER JOIN
                         dbo.ConfigurationSettingValue ON dbo.ConfigurationSettingValue.ConfigurationSettingId = dbo.ConfigurationSetting.ConfigurationSettingId AND 
                         dbo.ConfigurationSettingValue.ConfigValueId = dbo.ConfigValue.ConfigValueId

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[29] 4[32] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Environment"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 118
               Right = 227
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ConfigValue"
            Begin Extent = 
               Top = 6
               Left = 265
               Bottom = 135
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ConfigurationTemplate"
            Begin Extent = 
               Top = 6
               Left = 473
               Bottom = 118
               Right = 696
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ConfigurationSetting"
            Begin Extent = 
               Top = 120
               Left = 38
               Bottom = 249
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ConfigurationSettingValue"
            Begin Extent = 
               Top = 144
               Left = 472
               Bottom = 309
               Right = 712
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_ConfigPerEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      Column = 4800
         Alias = 900
         Table = 7665
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_ConfigPerEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_ConfigPerEnvironment';

