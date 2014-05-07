using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Processor.Enum;

namespace Configoro.Net.Processor.Processor
{
    public static class ProcessorFactory
    {
        private static List<ProcessorTypeItem> _processors;
        public static List<ProcessorTypeItem> processors
        {
            get
            {
                if (_processors == null)
                {
                    _processors = new List<ProcessorTypeItem>();
                    _processors.Add(new ProcessorTypeItem() { type = ProcessorType.EditNodeValue, processor = new XMLProcessor() });
                    _processors.Add(new ProcessorTypeItem() { type = ProcessorType.EditNodeXML, processor = new XMLProcessor() });
                    _processors.Add(new ProcessorTypeItem() { type = ProcessorType.AddNode, processor = new XMLAddNodeProcessor() });
                    _processors.Add(new ProcessorTypeItem() { type = ProcessorType.DeleteNode, processor = new XMLDeleteNodeProcessor() });
                }
                return _processors;
            }
        }
        
        public static IProcessor GetProcessor(int number)
        {
            var obj = processors.Where(prop => (int)prop.type == number).FirstOrDefault();

            if(obj==null)
                throw new Exception("Processor type does not exist");

            return obj.processor;
        }
    }
}
