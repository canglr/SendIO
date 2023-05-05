using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SendIO.Domain.Entities;

namespace SendIO.Persistence.Context
{
	public class SendIOContext : DbContext
    {
		public const string DEFAULT_SCHEMA = "dbo";

		public SendIOContext()
		{
		}

		public SendIOContext(DbContextOptions options) : base(options)
		{

		}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Server=s2.gulernet.net;Database=sendio;User ID=sa;Password=jSGFutxvS9Cz;Trusted_Connection=False;Persist Security Info=False;Encrypt=False";
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }


        public DbSet<FileHead> FileHeads { get; set; }

		public DbSet<FileContent> FileContents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            var addedEntites = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added)
                .Select(i => (BaseEntity)i.Entity);
            PrepareAddedEntities(addedEntites);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreateDate == DateTime.MinValue)
                    entity.CreateDate = DateTime.Now;
            }
        }

    }
}

