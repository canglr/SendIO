using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendIO.Domain.Entities
{
	public abstract class BaseEntity
	{
        [Key]
        public Guid Id { get; set; }

		public DateTime CreateDate { get; set; }
	}
}

