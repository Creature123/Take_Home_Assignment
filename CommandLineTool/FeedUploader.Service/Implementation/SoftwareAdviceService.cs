using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FeedUploader.Service.Interfaces;
using FeedUploader.Service.Models;
using Microsoft.Extensions.Logging;

namespace FeedUploader.Service.Implementation
{
	public class SoftwareAdviceService : DataReader<SoftwareAdviceData>, ISoftwareAdviceService
	{

        private readonly ILogger<SoftwareAdviceService> _logger;
        private readonly IDataReader<SoftwareAdviceData> _datareader;

        public SoftwareAdviceService(ILoggerFactory loggerFactory, IDataReader<SoftwareAdviceData> reader)
		{
            _logger = loggerFactory.CreateLogger<SoftwareAdviceService>();
            _datareader = reader;

        }

        public async Task<SoftwareAdviceData> ExtractData(string filepath)
        {
          //  DataReader<SoftwareAdviceData> s = new DataReader<SoftwareAdviceData>();
            var fileExtension = valiDateFileExtension(filepath);
            if (fileExtension.Equals(UtilConstant.json))
            {

                return await Task.Run(() =>
                 _datareader.ExtractJsonFile(filepath));
            }
            else
            {
                return new SoftwareAdviceData();
            }
        }

        public async Task PrintReader(SoftwareAdviceData datadescription)
        {
            await Task.Run(() =>
               PrintValue(datadescription)
            );
        }

        private void PrintValue(SoftwareAdviceData reader)
        {
            foreach (var result in reader.products)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Importing : Category : \"");
                foreach (var category in result.categories)
                {
                    Console.Write($"{category},");
                }
                Console.Write("\"; ");
                Console.Write($" title: {result.title}" + ";" + $"Twitter: {result.twitter}" + ";");
                Console.WriteLine();
                Console.ResetColor();
            }

        }


        private string valiDateFileExtension(string filepath)
        {
            try
            {
                FileInfo path = new FileInfo(filepath);
                return path.Extension;
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File Not found! during extracting the extension of the file");
                return null;
            }

        }
    }
}

