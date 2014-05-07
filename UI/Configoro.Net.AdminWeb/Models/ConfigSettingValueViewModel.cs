using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Configoro.Net.AdminWeb.Models
{
    public class ConfigSettingValueViewModel
    {
        public int ConfigurationSettingValueId { get; set; }

        public int ConfigurationSettingId { get; set; }
        
        public int ConfigValueId { get; set; }
        public int EnvironmentId { get; set; }
        public string ConfigValue { get; set; }
    }
}