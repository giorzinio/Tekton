using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Domain.Entities;

namespace Tekton.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Stock)
                .IsRequired();
            
            builder.Property(t => t.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasPrecision(9, 2)
                .IsRequired();
        }
    }
}
