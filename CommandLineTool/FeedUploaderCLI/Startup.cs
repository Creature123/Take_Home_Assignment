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
			var provider = new ServiceCollection()
				.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddScoped<ICapterraService, CapterraService>()
				.AddScoped<ISoftwareAdviceService, SoftwareAdviceService>()
				.AddTransient(typeof(IDataReader<>), typeof(DataReader<>))
				.BuildServiceProvider();
			
			return provider;
		}
	}
}

