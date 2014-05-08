using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Configoro.Net.Domain;
using Configoro.Net.Domain.Interface;

namespace Configoro.Net.Processor.Processor
{
    public class XMLAddNodeProcessor : BaseNodeConfig, IProcessor
    {
        public bool ConvertDocument(IFileLoader file, List<Domain.ConfigView> config)
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
                    
                    var xmlnode = itm.Cast<XmlNode>().First();
                    doc.CreateTextNode(cf.value);
                    //only add the node if it doesnt already exist
                    if (xmlnode.InnerXml.IndexOf(cf.value) == -1)
                    {
                        xmlnode.InnerXml += "\n" + cf.value;
                    }

                }
                else
                {
                    throw new Exception("Error, could not find xpath " + cf.xPath);
                }

            }
        }
    }
}
