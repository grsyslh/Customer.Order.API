using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entity;

namespace Order.DataAccess.Context.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Barcode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
