using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace FeedUploaderCLI
{
    class Program
    {
        static async  Task<int> Main(string[] args)
        {
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




            capterraCommand.SetHandler((optionArgumentvalue) =>
            {
                Console.WriteLine($"Capterra File option {optionArgumentvalue}");
            }, fileOption);


            softwareadviceCommand.SetHandler((optionArgumentvalue) =>
            {
                Console.WriteLine($"softwareadvice File option {optionArgumentvalue}");
            }, fileOption);


            return await rootCommand.InvokeAsync(args);
        }
    }
}

