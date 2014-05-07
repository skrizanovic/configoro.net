
using System.Collections.Generic;


namespace Configoro.Net.Domain
{
    public class ConfigurationSetting
    {
        public int ConfigurationSettingId { get; set; }
        public int ConfigurationTemplateId { get; set; }
        public virtual ConfigurationTemplate ConfigurationTemplate { get; set; } 
        public string XpathValue { get; set; }
        public string ChangePropertyName { get; set; }
        public int ProcessorTypeId { get; set; }
        public virtual ProcessorType ProcessorType { get; set; }
        public virtual ICollection<ConfigurationSettingValue> ConfigurationSettingValues { get; set; }
        
    }
}
