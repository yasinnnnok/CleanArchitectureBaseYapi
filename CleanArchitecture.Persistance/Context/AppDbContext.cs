using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CleanArchitecture.Persistance.Context
{
    public sealed class AppDbContext : DbContext
    {


        //Bu yöntem ile .Connection ımı appSettingste tutmanın yolunu tutar. 
    
        public AppDbContext(DbContextOptions options) : base(options) { }

        //Entitylerimize ait özelleştirme yapabildiğimiz alan.ModelCreating
        //PErsisstance assembly sini verirsem. Otomatikmen DBsetleri yapmış olacak.
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefence).Assembly);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entires = ChangeTracker.Entries<Entity>();
            foreach (var entry in entires)
            {
                if (entry.State == EntityState.Added)
                    entry.Property(p => p.CreatedDate).CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;

            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
