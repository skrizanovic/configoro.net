
using System.Collections.Generic;


namespace Configoro.Net.Domain
{
    public class Environment
    {
        public int EnvironmentId { get; set; }
        public string EnvironmentName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ConfigValue> ConfigValues { get; set; }
    }
}
