using System;
using FeedUploader.Service.Implementation;
using FeedUploader.Service.Interfaces;
using FeedUploader.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FeedUploaderCLI
{
	public class Startup
	{
		public static IServiceProvider ConfigureService()
		{
			// Adding App Service as DI
			var provider = new ServiceCollection()
				.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddScoped<ICapterraService, CapterraService>()
				.AddScoped<ISoftwareAdviceService, SoftwareAdviceService>()
				.AddScoped(typeof(IDataReader<>), typeof(DataReader<>))
				.BuildServiceProvider();
			
			return provider;
		}
	}
}

