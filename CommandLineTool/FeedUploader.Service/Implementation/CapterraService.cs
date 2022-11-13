using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using FeedUploader.Service.Interfaces;
using FeedUploader.Service.Models;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FeedUploader.Service.Implementation
{
	public class CapterraService : DataReader<CapterraData>, ICapterraService
	{
		public CapterraService()
		{
		}

        public async Task<List<CapterraData>>  ExtractData(string filepath)
        {
            DataReader<CapterraData> s = new DataReader<CapterraData>();
            var fileExtension = valiDateFileExtension(filepath);
            if (fileExtension.Equals(UtilConstant.yaml))
            {

                return await Task.Run(() =>
                 s.ExtractYamlFile(filepath));
            }
            else
            {
                return new List<CapterraData>();
            }

        }

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

        private string valiDateFileExtension(string filepath)
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

