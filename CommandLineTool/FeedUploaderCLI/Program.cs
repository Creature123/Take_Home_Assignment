using System;
using System.CommandLine;
using System.Threading.Tasks;
using FeedUploader.Service.Implementation;
using FeedUploader.Service.Models;

namespace FeedUploaderCLI
{
    class Program
    {
        static async  Task<int> Main(string[] args)
        {

            CapterraService data = new CapterraService();
            SoftwareAdviceService sdata = new SoftwareAdviceService();
           
            var rootCommand = new RootCommand(description: "Command Line tool to import Project file from different sources :");

            var importCommand = new Command("import", "Use this command to import files");
            importCommand.AddAlias("i");
            rootCommand.Add(importCommand);

            var fileOption = new Option<string>
               ("--filepath", "An option to pass filepath");
            fileOption.IsRequired = true;
            fileOption.AddAlias("--f");
            fileOption.AddAlias("--file");

            var capterraCommand = new Command("capterra", "Use this command to import files from capterra site");
            capterraCommand.AddAlias("-c");
            capterraCommand.AddOption(fileOption);


            var softwareadviceCommand = new Command("softwareadvice", "Use this command to import files from softwaredevice site");
            softwareadviceCommand.AddAlias("-s");
            softwareadviceCommand.AddOption(fileOption);





            importCommand.Add(
                capterraCommand
                );

            importCommand.Add(
                softwareadviceCommand
                );




            capterraCommand.SetHandler(async (optionArgumentvalue) =>
            {
                Console.WriteLine($"Capterra File option {optionArgumentvalue}");

                var result = await data.ExtractData(optionArgumentvalue);
                if(result.Count> 0)
                {
                   await data.PrintReader(result);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Something went Wrong");
                }
            }, fileOption);


            softwareadviceCommand.SetHandler(async (argument) =>
            {
                Console.WriteLine($"SoftwareAdvice File option {argument}");
                
                var result = await sdata.ExtractData(argument.ToString());
                if (result != null)
                {
                    await sdata.PrintReader(result);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Something went Wrong");
                }
            }, fileOption);


            return await rootCommand.InvokeAsync(args);
        }
    }
}

