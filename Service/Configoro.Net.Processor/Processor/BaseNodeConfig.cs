using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Configoro.Net.Domain;
using System.IO;

namespace Configoro.Net.Processor.Processor
{
    public abstract class BaseNodeConfig
    {
        public System.IO.Stream ConvertDocument(System.IO.Stream file, List<Domain.ConfigView> config)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            
            doc.Load(file);
            ChangeContent(config, doc);
            MemoryStream returnStream = new MemoryStream();
            doc.Save(returnStream);

            returnStream.Position = 0;
            return returnStream;
        }

        public bool ConvertDocument(string file, List<Domain.ConfigView> config)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;

            doc.Load(file);
            ChangeContent(config, doc);
            doc.Save(file);

            return true;
        }
        protected abstract void ChangeContent(List<ConfigView> config, XmlDocument doc);
    }
}
