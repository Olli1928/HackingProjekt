using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HackingProjekt.Data;
using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HackingProjekt.Pages.MoviesScafold
{
    public class IndexModel : PageModel
    {
        private readonly HackingProjekt.Data.HackingProjektDBContext _context;

        public IndexModel(HackingProjekt.Data.HackingProjektDBContext context)
        {
            _context = context;
        }

        public List<movie> Movie { get;set; } = default!;

       
        [BindProperty(SupportsGet = true)]
        public aldersFilter AldersFilter { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            // Alders filtering
            movies = movies.Where(s =>
           (AldersFilter.A && s.Aldersgrænse == "A") ||
           (AldersFilter.Syv && s.Aldersgrænse == "7") ||
           (AldersFilter.Elleve && s.Aldersgrænse == "11") ||
           (AldersFilter.Femten && s.Aldersgrænse == "15")
    );


            Movie = await movies.ToListAsync();
        }

       
    }
}
