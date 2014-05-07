using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configoro.Net.Domain.Interface
{
    public interface IFileLoader
    {
        bool FileExists { get; }
        string fileName { get; set; }
        string AbsolutefileName { get; set; }
        string fileContent { get;  }
        void Save(string content);
        int Length { get; }
    }
}
