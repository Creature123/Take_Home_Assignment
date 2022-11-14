
# FeedUploaderCLI


FeedUploaderCLI is a Command Line tool which can be used to upload/process setting file with different file format.




## Project Structure


``` bash
.
├── FeedUploader.Service                # All the Interfaces, Models, Implementations are here (Service Layer )
|   ├── Implementations
|   |   ├── CapterraService.cs          # Service layer for processing the file from site 'Capterra'          
|   |   ├── DataReader.cs               # A generic Class Implementation for of IDataReader interface to process data in both 'Json' & 'yaml' format
|   |   └── SoftwareAdviceService.cs    # Service layer for processing the file from site 'SoftwareAdvice'   
|   ├── Interfaces
|   |   ├── ICapterraService.cs
|   |   ├── IDataReader.cs
|   |   └── ISoftwareAdviceService.cs
|   └── Models
|       ├── CapterraData.cs             # Data defination for 'Capterra' Service Data.
|       ├── SoftwareAdviceData.cs       # Data defination for 'SoftwareAdvice' Service Data.
|       └── UtilConstant.cs             # Useful constant variables
|
├── FeedUploader.Test                   # Unit Testing Project.                  
|   ├── InputFile
|   └── CapterraServiceTest.cs          # Unit test Scripts for testing 'CapterraService' & 'DataReader' layer
|
└── FeedUploaderCLI                     # Main CLI Project which has the reference of 'FeedUploader.Service'
    ├── nupkg                           # CLI package file to upload globally as a Nuget package.
    ├── Program.cs                      # Driver Class , all the command level code are here.
    └── Startup.cs                      # Dependency Injection Class

```
## Installation

#### 1. Colone the repository
```bash
  https://github.com/Creature123/Take_Home_Assignment.git

```

#### 2. Go to "CommandLineTool" folder and open Visual Studio / Terminal from FeedUplaoderCLI

``` bash
  ~ cd FeedUploaderCLI

  ~ FeedUploaderCLI > dotnet build

```

### 3. To pack the project for Nuget Release

``` bash
` FeedUploader CLI > dotnet pack

```


    
