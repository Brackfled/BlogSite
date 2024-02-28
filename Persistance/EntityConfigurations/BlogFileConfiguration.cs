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
    public class BlogFileConfiguration : IEntityTypeConfiguration<BlogFile>
    {
        public void Configure(EntityTypeBuilder<BlogFile> builder)
        {
            builder.ToTable("BlogFiles").HasKey(bf => bf.Id);

            builder.Property(bf => bf.Id).HasColumnName("Id").IsRequired();
            builder.Property(bf => bf.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(bf => bf.Name).HasColumnName("Name").IsRequired();
            builder.Property(bf => bf.FilePath).HasColumnName("FilePath").IsRequired();
            builder.Property(bf => bf.FileUrl).HasColumnName("FileUrl").IsRequired();
            builder.Property(bf => bf.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(bf => bf.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(bf => bf.DeletedDate).HasColumnName("DeletedDate");

            builder.HasOne(bf => bf.User);

            builder.HasQueryFilter(bf => !bf.DeletedDate.HasValue);

        }
    }
}
