using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Entities;

namespace SalesManagementApp.Data
{ //Entitiesde Class açıp prop mrop doldurduktan sonra burada dbcontexte çağırıoruz
    public class SalesManagementDbContext:DbContext
    {
        public SalesManagementDbContext(DbContextOptions<SalesManagementDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData.AddEmployeeData(modelBuilder); 
            SeedData.AddProductData(modelBuilder);
            SeedData.AddClientData(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; } //bu bölümden sonra migration ekliyoruz
        public DbSet<EmployeeJobTitle> EmployeeJobTitles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RetailOutlet> RetailOutlets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SalesOrderReport> SalesOrderReports { get; set; }

    }
}
