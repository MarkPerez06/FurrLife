using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FurrLife.Models;

namespace FurrLife.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<CartProducts> CartProducts { get; set; }
        public DbSet<CartProductsView> CartProductsView { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<OrderProductsView> OrderProductsView { get; set; }
        public DbSet<Discounts> Discounts { get; set; }
        public DbSet<Persons> Persons { get; set; }
        public DbSet<PetHealthRecord> PetHealthRecord { get; set; }
        public DbSet<ImmunizationHistory> ImmunizationHistory { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }
    }
}