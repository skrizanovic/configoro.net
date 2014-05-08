using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Domain.Interface;
using System.IO;
using System.Reflection;

namespace Configoro.Net.Processor.Helper
{
    public class FileLoader : IFileLoader
    {
        public bool FileExists
        {
            get
            {
                return File.Exists(fileName);
            }
           
        }
        string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set { SetFileName(value); }
        }

        protected string _fileContent;
        public string Content
        {
            get
            {
                if(_fileContent==null)
                    _fileContent= System.IO.File.ReadAllText(fileName);

                return _fileContent;
            }
           
        }

        public virtual void Save(string content)
        {
            File.WriteAllText(AbsolutefileName, content);
        }
        private void SetFileName(string file)
        {
            _fileName = file;

            //* is an illegal char, so replace with && and then replace back to * once absolute path is found
            AbsolutefileName = System.IO.Path.GetFullPath(file.Replace("*","&&")).Replace("&&","*");
            
            
        }
       




        public int Length
        {
            get { return fileName.Length; }
        }


        public string AbsolutefileName{get;set;}
    }
}
