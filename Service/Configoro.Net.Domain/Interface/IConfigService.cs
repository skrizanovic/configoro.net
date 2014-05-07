using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Domain;

namespace Configoro.Net.Domain.Interface
{
    public interface IConfigService
    {
        List<ConfigView> GetSettings(string EnvironmentName, string templateName);
    }
}
