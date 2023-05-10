using System;
namespace SendIO.WebUI.Model
{
	public class FileHeadResult
	{
        public string? title { get; set; }

        public string? description { get; set; }

        public DateTime enddate { get; set; }

        public ICollection<FileContentResult> FileContents { get; set; }
    }
}

