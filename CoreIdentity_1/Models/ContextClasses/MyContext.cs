using CoreIdentity_1.Models.Configurations;
using CoreIdentity_1.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreIdentity_1.Models.ContextClasses
{

    //Identity kullanacak iseniz IdentityDbContext class'ından miras almalısınız ve onun generic tiplerini belirlediginiz yapıları vermek zorundasınız (ozel olarak sekillendirdigini class tiplerine dikkat etmelisiniz)
    public class MyContext : IdentityDbContext<AppUser,AppRole,int,AppUserClaim,AppUserRole,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {

        public MyContext(DbContextOptions<MyContext> opt) :base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new AppUserRoleConfiguration());
            builder.ApplyConfiguration(new ProfileConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new AppUserClaimConfiguration());

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<AppUserProfile> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<AppUserClaim> AppUserClaims { get; set; }


    }
}
