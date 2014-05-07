using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configoro.Net.Domain
{
    public class ConfigValue
    {
        public int ConfigValueId { get; set; }
        public int EnvironmentId { get; set; }
        public virtual Environment Environment { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ConfigurationSettingValue> ConfigurationSettingValues { get; set; }
    }
}
