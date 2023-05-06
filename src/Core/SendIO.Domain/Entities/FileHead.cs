using System;
namespace SendIO.Domain.Entities
{
	public class FileHead: BaseEntity
	{
		public string title { get; set; }

		public string description { get; set; }

		public DateTime enddate { get; set; }

		public ICollection<FileContent> FileContents { get; set; }
	}
}

