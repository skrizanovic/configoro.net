using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Domain.Interface;
using Configoro.Net.Processor.Helper;

namespace Configoro.Net.UnitTests.Helper
{
    public class FileLoaderWithoutSave : FileLoader
    {
        public override void Save(string content)
        {
            //do nothing with the save
            _fileContent = content;
        }
    }
}
