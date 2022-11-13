using System;
using FeedUploader.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedUploader.Service.Interfaces
{
	public interface ISoftwareAdviceService
	{
        public Task<SoftwareAdviceData> ExtractData(string filepath);
        public Task PrintReader(SoftwareAdviceData datadescription);
    }
}

