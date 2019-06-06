using EF.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EF.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<StoreDO> Stores { get; set; }
        public DbSet<OrderDO> Orders { get; set; }
        public DbSet<UserDO> Users { get; set; }
        public DbSet<SessionDO> Sessions { get; set; }
        public DbSet<MenuDO> Menus { get; set; }
        public DbSet<SessionStoreDO> SessionsStores { get; set; }

        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
