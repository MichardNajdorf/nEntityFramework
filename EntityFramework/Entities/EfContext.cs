using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Entities
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Chinook");
            }
            
        }


        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WorkItemState> WorkItemStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>()
                .Property(x => x.State)
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(T => T.Activity)
                .HasMaxLength(200);


            modelBuilder.Entity<Address>()
                .Property(x => x.Country)
                .HasColumnType("varchar(70)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
            
                eb.Property(wi => wi.Priority).HasDefaultValue(1);
                eb.HasMany(W => W.Comments)
                    .WithOne(C => C.Workitem)
                    .HasForeignKey(C => C.WorkItemId);
                eb.HasOne(w => w.User)
                    .WithMany(U => U.WorkItems)
                    .HasForeignKey(w => w.UserGuid);
                eb.HasMany(w => w.Tags)
                    .WithMany(t => t.WorkItems)
                    .UsingEntity<WorkItemTag>(
                        w=>w.HasOne(wit=>wit.Tag)
                            .WithMany()
                            .HasForeignKey(wit=>wit.TagId),
                        w=>w.HasOne(wit=>wit.WorkItem)
                            .WithMany()
                            .HasForeignKey(wit=>wit.WorkItemId),
                        wit =>
                        {
                            wit.HasKey(x => new { x.TagId, x.WorkItemId });
                            wit.Property(x => x.PubblicationDate).HasDefaultValueSql("getutcdate()");
                        }
                        );

            });
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();
                eb.HasOne(c => c.Author)
                    .WithMany(a => a.Comments)
                    .HasForeignKey(c => c.AuthorId);


            });
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<WorkItemState>(wis =>
            {
                wis.HasMany(w => w.WorkItems)
                    .WithOne(WI => WI.WorkItemState)
                    .HasForeignKey(WI => WI.WorkItemStateId);
            });
        }
    }
}
