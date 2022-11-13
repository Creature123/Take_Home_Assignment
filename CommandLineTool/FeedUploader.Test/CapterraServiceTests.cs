using System;
using Xunit;
using FeedUploader.Service.Implementation;
using FeedUploader.Service.Interfaces;
using FeedUploader.Service.Models;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FeedUploader.Test
{
	public class CapterraServiceTests
	{
		private readonly CapterraService _sut;
        private readonly Mock<IDataReader<CapterraData>> _capterraDatareadermock = new Mock<IDataReader<CapterraData>>();
        private readonly Mock<ILogger<CapterraService>> _logger = new Mock<ILogger<CapterraService>>();
		private readonly Mock<ILoggerFactory> loggerFactory = new Mock<ILoggerFactory>();
		private readonly Mock<DataReader<CapterraData>> _dataReader;
		//private readonly Mock<IDataReader<CapterraData>> mock;

		public CapterraServiceTests()
		{
			//_logger = loggerFactory.CreateLogger<CapterraService>();
			
			_sut = new CapterraService(loggerFactory.Object, _capterraDatareadermock.Object);

           
            _dataReader = new Mock<DataReader<CapterraData>>();

        }
		[Fact]
		public void valiDateFileExtension_ShouldReturnFileExtension()
		{
			// Arrange
			var filepath = "/Users/souravdas/Projects/YamlReader/input/capterra.yaml";

			// Act
			var result = _sut.valiDateFileExtension(filepath);

			// Assert	
			Assert.Equal(".yaml", ".yaml");
			
			
		}

		[Fact]
		public  void   CapterraExtractDataShouldReturnSameValue()
		{



			// Arrange
			var testData = new List<CapterraData>();

			testData.Add(new CapterraData("Bugs & Issue Tracking,Development Tools", "GitGHub", "github"));
			testData.Add(new CapterraData("Instant Messaging & Chat,Web Collaboration,Productivity", "Slack", "slackhq"));
			testData.Add(new CapterraData("Project Management,Project Collaboration,Development Tools", "IRA Software", "jira"));

			var filepath = "/Users/souravdas/Projects/YamlReader/input/capterra.yaml";

			// Act
			var result = _dataReader.Object.ExtractYamlFile(filepath);

			// Assert
			Assert.Equal(testData[0].tags, result[0].tags);

		}

    }
}

