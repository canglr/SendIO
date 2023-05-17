using System;
namespace SendIO.WebUI.Model
{
	public class FileContentResult
	{
        public Guid Id { get; set; }

        public Guid FileHeadId { get; set; }

        public string originalname { get; set; }

        public string generatedname { get; set; }

        public long size { get; set; }

        public string type { get; set; }
    }
}

