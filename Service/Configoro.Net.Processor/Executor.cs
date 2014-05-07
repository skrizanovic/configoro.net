using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Domain;
using System.IO;
using Configoro.Net.Processor.Processor;
using Ionic.Zip;
using Configoro.Net.Domain.Interface;
using Configoro.Net.Processor.Helper;


namespace Configoro.Net.Processor
{
    public class Executor
    {
        IConfigService _service;
        IFileLoader _file;
        public Executor()
        {
            _service = new ConfigService();
            _file = new FileLoader();
        }
        public Executor(IConfigService service, IFileLoader file)
        {
            _service = service;
            _file = file;
        }
        
        public bool ConvertConfigFile(string environment, string template, string file, string zipFileName)
        {
            IProcessor processor = null;
            bool success = true;

            _file.fileName = file;

            var configSettings = _service.GetSettings(environment, template);

            foreach(var procType  in configSettings.Select(p=>p.ProcessorTypeId).Distinct().ToList())
            {
                processor = ProcessorFactory.GetProcessor(procType);
                success &= ExecuteProcessor(processor, configSettings.Where(p => p.ProcessorTypeId == procType).ToList(), _file, zipFileName);

            }
            return success;

        }

        private bool ExecuteProcessor(IProcessor processor, List<ConfigView> configSettings, IFileLoader file, string zipFileName)
        {
           


            if (configSettings.Count == 0)
            {
                throw new Exception("No Configuration setting found");
            }


            if (!string.IsNullOrEmpty(zipFileName))
            {
                var zfile = new FileLoader();
                zfile.fileName = zipFileName;

                var parts = zfile.AbsolutefileName.Split('\\').Length;

                var filePart = zfile.AbsolutefileName.Split('\\').Last();
                var dirPart = zfile.AbsolutefileName.Remove(zfile.AbsolutefileName.IndexOf(filePart));

                var files = Directory.GetFiles(dirPart, filePart);

                if (files.Length > 1)
                {
                    throw new Exception(string.Format("Can not find file specified, criteria '{0}' is too wide. Found {1} results", filePart, file.Length));
                }
                if (file.Length == 0)
                {
                    throw new Exception(string.Format("No files found matching criteria '{0}', in folder '{1}'", filePart, dirPart));
                }
                ZipFile zipFile = new ZipFile(files[0]);
                Stream ms = null;

                Console.WriteLine("Found zip file");
                var ze = zipFile[file.fileName];
                if (ze != null)
                {
                    Console.WriteLine("Found file within ZipFile");
                    ms = new MemoryStream();
                    ze.Extract(ms);
                    ms.Position = 0;
                    Stream returnStream = processor.ConvertDocument(ms, configSettings);

                    zipFile.RemoveEntry(file.fileName);

                    var dir = file.fileName.Substring(0, file.fileName.IndexOf(file.fileName.Split('/').Last()));

                    zipFile.AddEntry(file.fileName, dir, returnStream);
                    zipFile.Save();
                    Console.WriteLine(string.Format("Saved Zip File '{0}'",zipFile.TempFileFolder));
                }




                return true;
            }
            else
            {
                if (!file.FileExists)
                {
                    throw new Exception("File does not exist");
                }
                return processor.ConvertDocument(file.fileName, configSettings);

            }
        }
       

    }
}
