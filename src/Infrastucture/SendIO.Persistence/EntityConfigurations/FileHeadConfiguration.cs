using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SendIO.Domain.Entities;
using SendIO.Persistence.Context;

namespace SendIO.Persistence.EntityConfigurations
{
	public class FileHeadConfiguration: BaseEntityConfiguration<FileHead>
	{
        public override void Configure(EntityTypeBuilder<FileHead> builder)
        {
            base.Configure(builder);

            builder.ToTable("filehead", SendIOContext.DEFAULT_SCHEMA);

            
        }
    }
}

