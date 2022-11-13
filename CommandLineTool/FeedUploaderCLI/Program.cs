using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.IO;
using System.Threading.Tasks;
using FeedUploader.Service.Implementation;
using FeedUploader.Service.Interfaces;
using FeedUploader.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FeedUploaderCLI
{
    class Program
    {
        static async  Task<int> Main(string[] args)
        {


            // Added Dependency Injection
            var container = Startup.ConfigureService();

            var logger = container.GetService<ILoggerFactory>().CreateLogger<Program>();
          

            var _capterraservice = container.GetRequiredService<ICapterraService>();
            var _softwareservice = container.GetRequiredService<ISoftwareAdviceService>();


            /// <summary>
            /// Command Creation Started
            ///
            /// <CLItoolname> <import> <serviceName> [--FileOptions] [FilePath]
            /// Used Nuget Package System.CommandLine 
            /// </summary>


            #region Command Section Started

            // root Command
            var rootCommand = new RootCommand(description: "Command Line tool to import Project file from different sources :");

            // import Sub Command 
            var importCommand = new Command("import", "Use this command to import files");
            importCommand.AddAlias("i");
            rootCommand.Add(importCommand);

            // File Option creation and made it mandatory
            var fileOption = new Option<string>
               ("--filepath", "An option to pass filepath");
            fileOption.IsRequired = true;
            fileOption.AddAlias("--f");
            fileOption.AddAlias("--file");

            // Capterra Sub command <serviceName>
            var capterraCommand = new Command("capterra", "Use this command to import files from capterra site");
            capterraCommand.AddAlias("-c");
            capterraCommand.AddOption(fileOption);

            // SoftwareAdvice Sub command <serviceName>
            var softwareadviceCommand = new Command("softwareadvice", "Use this command to import files from softwaredevice site");
            softwareadviceCommand.AddAlias("-s");
            softwareadviceCommand.AddOption(fileOption);

            // Adding both Sub command under Import command
            /// command will work like  ==>  import [capterra/-c] / [softwareadvice/-s]
            importCommand.Add(
                capterraCommand
                );

            importCommand.Add(
                softwareadviceCommand
                );

            // addding the Handler Section to work with the command line argument which user will pass
            capterraCommand.SetHandler(async (optionArgumentvalue) =>
            {
               // Console.WriteLine($"Capterra File option {optionArgumentvalue}");

                var result = await _capterraservice.ExtractData(optionArgumentvalue);
                if(result.Count> 0)
                {
                   await _capterraservice.PrintReader(result);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Something went Wrong");
                }
            }, fileOption);

            // addding the Handler Section to work with the command line argument which user will pass
            softwareadviceCommand.SetHandler(async (argument) =>
            {
               // Console.WriteLine($"SoftwareAdvice File option {argument}");
                
                var result = await _softwareservice.ExtractData(argument.ToString());
                if (result != null)
                {
                    await _softwareservice.PrintReader(result);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Something went Wrong");
                }
            }, fileOption);

            return await rootCommand.InvokeAsync(args);

            #endregion
        }
    }
}

