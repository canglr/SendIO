using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SendIO.Domain.Entities;
using SendIO.Persistence.Context;

namespace SendIO.Persistence.EntityConfigurations
{
    public class FileContentConfiguration : BaseEntityConfiguration<FileContent>
    {
        public override void Configure(EntityTypeBuilder<FileContent> builder)
        {
            base.Configure(builder);

            builder.ToTable("filecontent", SendIOContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.fileHead)
                .WithMany(i => i.FileContents)
                .HasForeignKey(i => i.FileHeadId);
        }
    }
}

