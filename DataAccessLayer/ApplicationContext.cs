using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace DataAccessLayer
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Innogotchi> Innogotchis { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserFarm> UserFarms { get; set; }
        public DbSet<DeadInnogotchi> DeadInnogotchis { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<InnogotchiBodyPart> InnogotchiBodyParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            modelBuilder.Entity<Farm>()
                .HasIndex(f => f.FarmName)
                .IsUnique();

            modelBuilder.Entity<Innogotchi>()
                .HasIndex(f => f.InnogotchiName)
                .IsUnique();

            modelBuilder.Entity<UserFarm>()
                .HasKey(uf => new { uf.UserId, uf.FarmId });

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "User" }
            );

            modelBuilder.Entity<BodyPart>().HasData(
                new BodyPart { BodyPartId = 1, BodyPartName = "Body" },
                new BodyPart { BodyPartId = 2, BodyPartName = "Eyes" },
                new BodyPart { BodyPartId = 3, BodyPartName = "Mouth" },
                new BodyPart { BodyPartId = 4, BodyPartName = "Nose" }
            );

            modelBuilder.Entity<InnogotchiBodyPart>().HasData(
                new InnogotchiBodyPart { InnogotchiBodyPartId  = 1, BodyPartId = 1, InnogotchiBodyPartNumber = 1, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Bodies\\body1.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 2, BodyPartId = 1, InnogotchiBodyPartNumber = 2, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Bodies\\body2.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 3, BodyPartId = 1, InnogotchiBodyPartNumber = 3, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Bodies\\body3.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 4, BodyPartId = 1, InnogotchiBodyPartNumber = 4, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Bodies\\body4.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 5, BodyPartId = 1, InnogotchiBodyPartNumber = 5, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Bodies\\body5.svg") },

                new InnogotchiBodyPart { InnogotchiBodyPartId = 6, BodyPartId = 2, InnogotchiBodyPartNumber = 1, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Eyes\\eyes1.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 7, BodyPartId = 2, InnogotchiBodyPartNumber = 2, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Eyes\\eyes2.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 8, BodyPartId = 2, InnogotchiBodyPartNumber = 3, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Eyes\\eyes3.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 9, BodyPartId = 2, InnogotchiBodyPartNumber = 4, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Eyes\\eyes4.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 10, BodyPartId = 2, InnogotchiBodyPartNumber = 5, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Eyes\\eyes5.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 11, BodyPartId = 2, InnogotchiBodyPartNumber = 6, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Eyes\\eyes6.svg") },

                new InnogotchiBodyPart { InnogotchiBodyPartId = 12, BodyPartId = 3, InnogotchiBodyPartNumber = 1, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Mouths\\mouth1.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 13, BodyPartId = 3, InnogotchiBodyPartNumber = 2, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Mouths\\mouth2.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 14, BodyPartId = 3, InnogotchiBodyPartNumber = 3, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Mouths\\mouth3.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 15, BodyPartId = 3, InnogotchiBodyPartNumber = 4, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Mouths\\mouth4.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 16, BodyPartId = 3, InnogotchiBodyPartNumber = 5, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Mouths\\mouth5.svg") },

                new InnogotchiBodyPart { InnogotchiBodyPartId = 17, BodyPartId = 4, InnogotchiBodyPartNumber = 1, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Noses\\nose1.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 18, BodyPartId = 4, InnogotchiBodyPartNumber = 2, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Noses\\nose2.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 19, BodyPartId = 4, InnogotchiBodyPartNumber = 3, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Noses\\nose3.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 20, BodyPartId = 4, InnogotchiBodyPartNumber = 4, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Noses\\nose4.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 21, BodyPartId = 4, InnogotchiBodyPartNumber = 5, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Noses\\nose5.svg") },
                new InnogotchiBodyPart { InnogotchiBodyPartId = 22, BodyPartId = 4, InnogotchiBodyPartNumber = 6, InnogotchiBodyPartImage = SVGToBase64(@"InnogotchiImages\\Noses\\nose6.svg") }
            );

            // Установление DeleteBehavior.Restrict для всех внешних ключей, чтобы предотвратить каскадное удаление.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        private static string SVGToBase64(string path)
        {
            byte[] imageBytes = File.ReadAllBytes(path);
            string base64Image = Convert.ToBase64String(imageBytes);
            return base64Image;
        }
    }
}