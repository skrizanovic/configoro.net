
using System.Collections.Generic;


namespace Configoro.Net.Domain
{
    public class ProcessorType
    {
        public int ProcessorTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ConfigurationSetting> ConfigurationSettings { get; set; }
    }
}
