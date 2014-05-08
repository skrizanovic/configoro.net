using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Configoro.Net.Domain;
using System.IO;
using Configoro.Net.Domain.Interface;

namespace Configoro.Net.Processor.Processor
{
    public class XMLProcessor : BaseNodeConfig, IProcessor
    {
        public bool ConvertDocument(IFileLoader file, List<ConfigView> config)
        {


            return base.ConvertDocument(file, config);
            
          
        }

        protected override void ChangeContent(List<ConfigView> config, XmlDocument doc)
        {
            foreach (var cf in config)
            {
                var itm = doc.SelectNodes(cf.xPath);

                if (itm.Count == 1)
                {
                    if (cf.ProcessorTypeId==2)
                    {
                        itm.Cast<XmlElement>().First().InnerXml = cf.value;
                    }
                    else
                    {
                        itm.Cast<XmlElement>().First().SetAttribute(cf.Property, cf.value);
                    }
                }
                else
                {
                    throw new Exception("Error, could not find xpath " + cf.xPath);
                }

            }
        }

        public System.IO.Stream ConvertDocument(System.IO.Stream file, List<Domain.ConfigView> config)
        {
            return base.ConvertDocument(file, config);
        }
    }
}
