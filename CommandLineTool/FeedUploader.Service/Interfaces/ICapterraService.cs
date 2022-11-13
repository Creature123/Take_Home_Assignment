using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeedUploader.Service.Models;

namespace FeedUploader.Service.Interfaces
{
	public interface ICapterraService
	{

		public Task<List<CapterraData>> ExtractData(string filepath);
		public Task PrintReader(List<CapterraData> datadescription);
	}
}

