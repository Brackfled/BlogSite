using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.EntityConfigurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subject").HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
            builder.Property(s => s.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(s => s.SubjectImageFileId).HasColumnName("SubjectImageFileId");
            builder.Property(s => s.CategoryId).HasColumnName("CategoryId").IsRequired();
            builder.Property(s => s.Title).HasColumnName("Title").IsRequired();
            builder.Property(s => s.Text).HasColumnName("Text").IsRequired();
            builder.Property(s => s.Summary).HasColumnName("Summary").IsRequired();
            builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(s => s.UpdatedDate).HasColumnName("UpdateDate");
            builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: p => p.Title, name: "UK_Subjects_Title").IsUnique();

            builder.HasOne(s => s.User);
            builder.HasOne(s => s.Category);
            builder.HasOne(s => s.SubjectImageFile);

            builder.HasQueryFilter(s => s.DeletedDate.HasValue);

            builder.HasBaseType((string)null!);
        }
    }
}
