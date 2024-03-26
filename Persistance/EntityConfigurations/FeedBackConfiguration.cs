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
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.ToTable("FeedBacks").HasKey(f => f.Id);

            builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
            builder.Property(f => f.Name).HasColumnName("Name").IsRequired();
            builder.Property(f => f.Email).HasColumnName("Email").IsRequired();
            builder.Property(f => f.Text).HasColumnName("Text").IsRequired();
            builder.Property(bf => bf.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(bf => bf.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(bf => bf.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: p => p.Name, name: "UK_FeedBacks_Name").IsUnique();

            builder.HasQueryFilter(f => !f.DeletedDate.HasValue);
        }
    }
}
