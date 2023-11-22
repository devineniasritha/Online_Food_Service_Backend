using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace OnlineFoodService.Models
{
    public class onlinefoodservicecontext: DbContext
    {
        public onlinefoodservicecontext(DbContextOptions<onlinefoodservicecontext> options)
           : base(options)
        {
        }

        public virtual DbSet<UserDetail> UserDetails { get; set; }

        public virtual DbSet<Menu> Menu { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
