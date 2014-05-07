
using System.Collections.Generic;


namespace Configoro.Net.Domain
{
    public class ConfigurationTemplate
    {
        public int ConfigurationTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string ConfigFile { get; set; }
        public int ConfigType { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ConfigurationSetting> ConfigurationSettings { get; set; }
    }
}
