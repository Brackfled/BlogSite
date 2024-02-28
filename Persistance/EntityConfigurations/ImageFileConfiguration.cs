using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.EntityConfigurations
{
    public class ImageFileConfiguration : IEntityTypeConfiguration<ImageFile>
    {
        public void Configure(EntityTypeBuilder<ImageFile> builder)
        {
            builder.ToTable("ImageFiles").HasKey(imf => imf.Id);

            builder.Property(imf => imf.Id).HasColumnName("Id").IsRequired();
            builder.Property(imf => imf.BlogFileId).HasColumnName("BlogFileId").IsRequired();
            builder.Property(imf => imf.ImageFileBracket).HasColumnName("ImageFileBracket").IsRequired();
            builder.Property(bf => bf.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(bf => bf.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(bf => bf.DeletedDate).HasColumnName("DeletedDate");

            builder.HasOne(imf => imf.BlogFile);

            builder.HasQueryFilter(imf => !imf.DeletedDate.HasValue);
        }
    }
}
