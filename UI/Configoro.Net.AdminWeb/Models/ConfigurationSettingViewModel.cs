using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Configoro.Net.AdminWeb.Models
{
    public class ConfigurationSettingViewModel
    {
        public int ConfigurationSettingId { get; set; }
        public int ConfigurationTemplateId { get; set; }
       
        public string XpathValue { get; set; }
        public string ChangePropertyName { get; set; }
        public int ProcessorTypeId { get; set; }

        public string ConfigValue { get; set; }
        
    }
}