
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

#### 1. Clone the repository
```bash
  https://github.com/Creature123/Take_Home_Assignment.git

```

#### 2. Go to "CommandLineTool" folder and open Visual Studio / Terminal from FeedUplaoderCLI

``` bash
  ~ cd FeedUploaderCLI

  ~ FeedUploaderCLI > dotnet build

```
![dotnetbuild](https://user-images.githubusercontent.com/13292898/201592005-ade5872d-7a2e-4ba3-a449-0a645d28eab4.png)

### 3. To pack the project for Nuget Release

``` bash
~ FeedUploader CLI > dotnet pack

```
![dotnetpack](https://user-images.githubusercontent.com/13292898/201592996-0336e711-18ad-4df4-a260-5b2749e54d66.png)


### 4. To install the package globally on your machine

``` bash

~ FeedUploader CLI > dotnet tool list --Global 

~ FeedUploader CLI > dotnetm tool install --global --add-source ./nupkg FeedUplaoderCLI

```
![InstallationAndRUnningPic](https://user-images.githubusercontent.com/13292898/201593079-caf62df0-08a6-420f-9439-9115a6e110a8.png)



### 5. To uninstall the package globally from your machine

``` bash
~ FeedUploader CLI > dotnet tool list --global
~ FeedUploader CLI > dotnet tool uninstall feeduploadercli --global

```
![packageUninstaill](https://user-images.githubusercontent.com/13292898/201593159-ee284472-2792-412d-9b8f-e062f916e09e.png)

    
## Running Tests

### A. To run the test from Terminal without installing the package


#### 1. For help document


```bash
  dotnet run -- --help

```
![helpcommand](https://user-images.githubusercontent.com/13292898/201593299-0e38c0a1-bab7-47d1-a379-304cfdf05d43.png)


#### 2. Usage of Import Command to upload file from Capterra Service

``` bash

dotnet run -- import capterra --file "filepath"

```
![CapterraCommand](https://user-images.githubusercontent.com/13292898/201594020-a6a180b1-e903-4a37-a8cf-3561b9f5333a.png)


### B. To run the test from Terminal after installing the package globally

``` bash

feeeduploader import -c -f "filepath"

```
![InstallationAndRUnningPic](https://user-images.githubusercontent.com/13292898/201594136-bbe6dd80-74cb-4ab3-9f33-1912915d1aed.png)


## Packages Used to build the Project

| **Package Name** | **Desciption**  |
| ------ | ------ |
| **System.CommandLine** | Used to create the Command Builder for the project |
| **NewtonSoft.Json** | For serializing and Deserializing the Json File |
| **Microsoft.Extensions.Logging** | Used Microsoft Logging Package |
| **Microsoft.Extensions.DependencyInjection** | Tp use dependency Injection |
| **YamlDotNet** | to Serialize and Deserialize the Yaml structured file |
