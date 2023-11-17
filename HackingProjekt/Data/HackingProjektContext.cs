using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HackingProjekt.modelLib;

namespace HackingProjekt.Data
{
    public class HackingProjektContext : DbContext
    {
        public HackingProjektContext (DbContextOptions<HackingProjektContext> options)
            : base(options)
        {
        }

        public DbSet<HackingProjekt.modelLib.Movie> Movie { get; set; } = default!;
        public DbSet<HackingProjekt.modelLib.User> User { get; set; } = default!;
    }
}
