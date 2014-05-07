using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Processor.Enum;

namespace Configoro.Net.Processor.Processor
{
    public class ProcessorTypeItem
    {
        public ProcessorType type { get; set; }
        public IProcessor processor { get; set; }
    }
}
