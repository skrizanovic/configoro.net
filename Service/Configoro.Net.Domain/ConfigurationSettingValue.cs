using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configoro.Net.Domain
{
    public class ConfigurationSettingValue
    {
        public int ConfigurationSettingValueId { get; set; }

        public int ConfigurationSettingId { get; set; }
        public virtual ConfigurationSetting ConfigurationSetting { get; set; }

        public int ConfigValueId { get; set; }
        public virtual ConfigValue ConfigValue { get; set; }
    }
}
