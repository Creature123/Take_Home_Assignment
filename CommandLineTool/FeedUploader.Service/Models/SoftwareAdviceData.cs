using System;
using System.Collections.Generic;

namespace FeedUploader.Service.Models
{
	public class SoftwareAdviceData
	{
		   public List<Product> products { get; set; }
    }


        public class Product
        {
            public List<string> categories { get; set; }
            public string twitter { get; set; }
            public string title { get; set; }
        }
    
}

