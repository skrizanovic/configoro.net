using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Configoro.Net.Domain;
using System.IO;

namespace Configoro.Net.Processor.Processor
{
    public class XMLDeleteNodeProcessor : BaseNodeConfig, IProcessor
    {
        public bool ConvertDocument(string file, List<Domain.ConfigView> config)
        {
            return base.ConvertDocument(file, config);
        }

        public System.IO.Stream ConvertDocument(System.IO.Stream file, List<Domain.ConfigView> config)
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
                    var xmlnode = itm.Cast<XmlElement>().First();
                    if (string.IsNullOrEmpty(cf.Property))
                    {
                        xmlnode.ParentNode.RemoveChild(xmlnode);
                    }
                    else
                    {
                        xmlnode.RemoveAttribute(cf.Property);
                    }
                   
                }
                else
                {
                    //do not throw if this value has already been deleted
                    //throw new Exception("Error, could not find xpath " + cf.xPath);
                }

            }
        }
    }
}
