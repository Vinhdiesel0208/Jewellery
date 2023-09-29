using System;
using Lib;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.RegularExpressions;
namespace Group3.Db
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) :base(options)
        {
		}
        public DbSet<Users> User { get; set; }
        public DbSet<BrandMst> BrandMsts { get; set; }
        public DbSet<CatMst> CatMsts { get; set; }                      
        public DbSet<Blogs> Blogs { get; set; }        
        public DbSet<Contact> Contact { set; get; }
        public DbSet<ProdMst> ProdMsts { get; set; }
        public DbSet<TypeMst> Types { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string str = "Server=localhost;Database=Jewellery;User Id=SA;Password=Password.1;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(str);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Cấu hình liên kết giữa ProdMst và các bảng khác
            modelBuilder.Entity<ProdMst>
                ().HasOne(p => p.ProductImage)
                .WithMany().
            HasForeignKey(p => p.Image_id);
            modelBuilder.Entity<ProdMst>()
                .HasOne(p => p.CatMst)
                .WithMany()
                .HasForeignKey(p => p.Cat_ID);

            modelBuilder.Entity<ProdMst>()
                .HasOne(p => p.BrandMst)
                .WithMany()
                .HasForeignKey(p => p.Brand_ID);

            modelBuilder.Entity<ProdMst>()
                .HasOne(p => p.TypeMst)
                .WithMany()
                .HasForeignKey(p => p.Type_Id);

            modelBuilder.Entity<ProdMst>()
                .HasOne(p => p.Gender)
                .WithMany()
                .HasForeignKey(p => p.Gender_Id);

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1,/* Username = "admin",*/ FullName = "admin", Password = "123", Email = "admin@gmail.com", Role = true, Address = "", PhoneNumber = "" },
                new Users { Id = 2, /*Username = "test",*/ FullName = "test", Password = "123", Email = "test@gmail.com", Role = false, Address = "", PhoneNumber = "" });
            // Thêm các quan hệ và cấu hình khóa ngoại khác tại đây

            base.OnModelCreating(modelBuilder);
        }
    }
}

