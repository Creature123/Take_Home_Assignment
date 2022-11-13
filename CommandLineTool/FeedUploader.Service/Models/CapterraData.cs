using System;
namespace FeedUploader.Service.Models
{
	public class CapterraData
	{
       

        public CapterraData(string tags, string name ,string twitter)
        {
            this.tags = tags;
            this.name = name;
            this.twitter = twitter;
        }

        public string tags { get; set; }
        public string name { get; set; }
        public string twitter { get; set; }
    }
}

