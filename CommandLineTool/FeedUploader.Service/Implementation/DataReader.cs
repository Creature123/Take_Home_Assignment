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
	public class DataReader<T> : IDataReader<T> where T : class
	{


        public T ExtractJsonFile(string filepath)
        {
            T reader;
            try
            {

                filepath = Path.GetFullPath(filepath);

                using (StreamReader r = new StreamReader(filepath))
                {
                    string data = r.ReadToEnd();
                    reader = JsonConvert.DeserializeObject<T>(data);

                }
                return reader;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex.Message}");
                Console.ResetColor();
                return null;
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File {ex.FileName} not found!");
                return null;
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong during Json Conversion : {ex.StackTrace}");
                Console.ResetColor();
                return null;

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong at the input file level: {ex.Data}");
                Console.ResetColor();
                return null;
            }


        }

        public List<T> ExtractYamlFile(string filepath)
        {

            List<T> reader = new List<T>();

            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                    .Build();


            try
            {

                filepath = Path.GetFullPath(filepath);


                using (StreamReader r = new StreamReader(filepath))
                {

                    var yamlobject = deserializer.Deserialize(r);

                    var serializer = new SerializerBuilder()
                    .JsonCompatible()
                    .Build();

                    var json = serializer.Serialize(yamlobject);

                    reader = JsonConvert.DeserializeObject<List<T>>(json);

                }
                return reader;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ex.Message}");
                Console.ResetColor();
                return new List<T>();
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File {ex.FileName} not found!");
                return new List<T>();
            }
            catch (JsonException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong during Json Conversion : {ex.Data}");
                Console.ResetColor();
                return new List<T>();

            }
            catch (YamlException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong during Yaml Conversion : {ex.Message}");
                Console.ResetColor();
                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Something went wrong at the input file level: {ex.Data}");
                Console.ResetColor();
                return new List<T>();
            }
        }

       
    }
}

