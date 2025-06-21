using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine); // لعرض SQL في الكونسول
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
         .HasOne(p => p.Pharmacy)
         .WithMany(ph => ph.Posts)
         .HasForeignKey(p => p.PharmacyId)
         .OnDelete(DeleteBehavior.Restrict);

            // منع الحذف التلقائي لعلاقات Request
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Requests)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.RequestingPharmacy)
                .WithMany()
                .HasForeignKey(r => r.RequestingPharmacyId)
                .OnDelete(DeleteBehavior.Restrict);

            // منع الحذف التلقائي لعلاقات CartItem
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Post)
                .WithMany()
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Pharmacy)
                .WithMany()
                .HasForeignKey(c => c.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
