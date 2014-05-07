using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Configoro.Net.Domain;

namespace Configoro.Net.AdminWeb.Models
{
    public class TemplateViewModel 
    {
        public List<ConfigurationTemplate> ConfigurationTemplates { get; set; }
    }
}
