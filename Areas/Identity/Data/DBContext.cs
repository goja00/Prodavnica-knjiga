using bookverse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bookverse.Data;

public class DBContext : IdentityDbContext<IdentityUser>
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }    
    public DbSet<CoverType> coverTypes { get; set; }  
    public DbSet<ApplicationUser> applicationUsers { get; set; }
    public DbSet<Shopping_Cart> shoppingCarts { get; set; }  
    public DbSet<Newsletter> newsletters { get; set; }  
    public DbSet<OrderHeader> orderHeaders { get; set; } 
    public DbSet<OrderDetails> orderDetails { get; set; }


}
