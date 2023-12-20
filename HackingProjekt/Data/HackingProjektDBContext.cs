
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



    namespace HackingProjekt.Data
    {
        public class HackingProjektDBContext : IdentityDbContext
        {
            public HackingProjektDBContext(DbContextOptions<HackingProjektDBContext> options)
                : base(options)
            {
            }

            public DbSet<HackingProjekt.modelLib.movie> Movie { get; set; } = default!;
       
    }
    }

