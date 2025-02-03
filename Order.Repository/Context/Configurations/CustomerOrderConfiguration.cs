using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entity;

namespace Order.DataAccess.Context.Configurations
{
    public class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
    {

        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
