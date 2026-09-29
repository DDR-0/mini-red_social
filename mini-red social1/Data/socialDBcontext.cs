using System.Net;
using Microsoft.EntityFrameworkCore;
using mini_red_social1.Models;
using mini_red_social1.Models.Obtenibles;

namespace mini_red_social1.Data
{



    public class socialDBcontext : DbContext
    {
        public socialDBcontext(DbContextOptions<socialDBcontext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Post> Posts { get; set; }

    }
}