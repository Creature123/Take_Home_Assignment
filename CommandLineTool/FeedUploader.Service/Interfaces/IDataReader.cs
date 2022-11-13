using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedUploader.Service.Interfaces
{
	public interface IDataReader<T> where T: class
	{

		public List<T> ExtractYamlFile(string filepath);
		public T ExtractJsonFile(string filepath);
	}
}

