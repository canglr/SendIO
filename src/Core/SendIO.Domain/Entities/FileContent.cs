using System;
namespace SendIO.Domain.Entities
{
	public class FileContent: BaseEntity
	{
        public Guid FileHeadId { get; set; }

        public string originalname { get; set; }

		public string generatedname { get; set; }

		public FileHead fileHead { get; set; }

	}
}

