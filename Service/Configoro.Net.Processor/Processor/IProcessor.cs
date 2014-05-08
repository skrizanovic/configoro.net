using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Domain;
using System.IO;
using Configoro.Net.Domain.Interface;

namespace Configoro.Net.Processor.Processor
{
    public interface IProcessor
    {
        bool ConvertDocument(IFileLoader file, List<ConfigView> config);
        Stream ConvertDocument(Stream file, List<ConfigView> config);
    }
}
