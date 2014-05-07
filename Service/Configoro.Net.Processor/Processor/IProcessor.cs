using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Domain;
using System.IO;

namespace Configoro.Net.Processor.Processor
{
    public interface IProcessor
    {
        bool ConvertDocument(string file, List<ConfigView> config);
        Stream ConvertDocument(Stream file, List<ConfigView> config);
    }
}
