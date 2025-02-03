using Microsoft.EntityFrameworkCore;
using Order.DataAccess.Context.Configurations;
using Order.Domain.Entity;
using Order.Domain.Entity.Base;

namespace Order.Repository.Context
{
    public class CustomerOrderPostreDbContext : DbContext
    {
        public CustomerOrderPostreDbContext(DbContextOptions<CustomerOrderPostreDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerOrderConfiguration());

            modelBuilder.Entity<CustomerOrder>()
               .HasMany(co => co.Products)
               .WithMany(p => p.Orders)
               .UsingEntity(j => j.ToTable("OrderProducts"));

            modelBuilder.Entity<CustomerOrder>()
                .HasOne(co => co.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(co => co.CustomerId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {

                UpdateCRUDTimeStamp();

                return (await base.SaveChangesAsync(true, cancellationToken));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void UpdateCRUDTimeStamp()
        {
            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is IBaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            Entry(entity).Property(x => x.ModifiedDate).IsModified = false;
                            entity.CreatedDate = DateTime.Now.ToUniversalTime();
                            break;
                        case EntityState.Modified:
                            Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                            entity.ModifiedDate = DateTime.Now.ToUniversalTime();
                            break;
                        case EntityState.Deleted:
                            Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                            entity.ModifiedDate = DateTime.Now.ToUniversalTime();
                            entity.IsDeleted = true;
                            Entry(entity).State = EntityState.Modified;
                            break;
                    }
                }
            }
        }

    }
}
