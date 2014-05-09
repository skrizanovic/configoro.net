using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Configoro.Net.AdminWeb.Models;
using Configoro.Net.Domain;

namespace Configoro.Net.AdminWeb.Transformer
{
    public static class ConfigSettingTransformer
    {
        public static ConfigSettingValueViewModel ToConfigSettingViewModel(this ConfigurationSettingValue config)
        {
            return new ConfigSettingValueViewModel()
            {
                ConfigurationSettingId = config.ConfigurationSettingId,
                ConfigurationSettingValueId = config.ConfigurationSettingValueId,
                ConfigValueId = config.ConfigValueId,
                EnvironmentId = config.ConfigValue.EnvironmentId,
                ConfigValue = config.ConfigValue.Name

            };
        }
        public static List<ConfigSettingValueViewModel> ToConfigSettingViewModel(this List<ConfigurationSettingValue> config)
        {
            var q = (from p in config
                     select p.ToConfigSettingViewModel()).ToList();
            return q;
        }
        public static ConfigurationSettingViewModel ToConfigurationSettingViewModel(this ConfigurationSetting config)
        {
            return new ConfigurationSettingViewModel()
            {
                ChangePropertyName = config.ChangePropertyName,
                ConfigurationSettingId = config.ConfigurationSettingId,
                ConfigValue = config.ConfigurationSettingValues!=null ? config.ConfigurationSettingValues.FirstOrDefault().ConfigValue.Name : "Unspecified",
                ProcessorTypeId = config.ProcessorTypeId,
                XpathValue = config.XpathValue
            };
        }
    }
}