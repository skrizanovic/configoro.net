using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Configoro.Net.Processor;
using Configoro.Net.Domain;
using Configoro.Net.Processor.Helper;



namespace Configoro.Net.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                
                var exe = new Executor(new ConfigService(),new FileLoader());

                var environment = args[0];
                var scheme = args[1];
                var file = args[2];
                string zipFile = "";

                if (args.Length > 3)
                {
                    zipFile = args[3];
                }
                

                


                if (!exe.ConvertConfigFile(environment, scheme, file, zipFile))
                {
                    //fail the build within Jenkins
                    Console.Write("exit 1");
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
                throw;
            }

            
           
        }
    }
}
