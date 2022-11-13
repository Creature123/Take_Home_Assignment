using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using FeedUploader.Service.Interfaces;
using FeedUploader.Service.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FeedUploader.Service.Implementation
{
	public class CapterraService : DataReader<CapterraData>, ICapterraService
	{
        private readonly ILogger<CapterraService> _logger;
        private readonly IDataReader<CapterraData> _datareader;

        #region CapterraService Constructor
        public CapterraService(ILoggerFactory loggerFactory, IDataReader<CapterraData> reader)
        {
            _logger = loggerFactory.CreateLogger<CapterraService>();
            _datareader = reader;
        }
        #endregion


        // Data Reader section to read data from yaml file
        /// <summary>
        ///   Here JsonExtractFile method can be calle based on filepath section
        /// </summary>
        /// <param name="filepath">User input</param>
        /// <returns>This will return Deserialized object from yaml input file</returns>
        public async Task<List<CapterraData>>  ExtractData(string filepath)
        {
           
            var fileExtension = valiDateFileExtension(filepath);
            if (fileExtension.Equals(UtilConstant.yaml))
            {
                
                var result = await Task.Run(() =>
                 _datareader.ExtractYamlFile(filepath));

                return result;
            }
            else
            {
                return new List<CapterraData>();
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="datadescription">List of data will be passed after Extracting the data from file</param>
        /// <returns>This method will print the extracted data from the file</returns>
        public async Task PrintReader(List<CapterraData> datadescription)
        {
            await Task.Run(() =>
               PrintValue(datadescription)
            );
        }

        private void PrintValue(List<CapterraData> datadescription)
        {
            if (datadescription.Count > 0)
            {
                foreach (var item in datadescription)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Importing : Name: \"{item.name}\"" + "; " + $"Categories: {item.tags}" + "; " + $"Twitter: @{item.twitter}");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>return file extension</returns>
        public string valiDateFileExtension(string filepath)
        {
            try
            {
               FileInfo path = new FileInfo(filepath);
                return  path.Extension;
            }
            catch(FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File Not found! during extracting the extension of the file");
              
                return null;
            }

        }
    }
}

