using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HackingProjekt.Data;
using HackingProjekt.modelLib;

namespace HackingProjekt.Pages.MoviesScafold
{
    public class DetailsModel : PageModel
    {
        private readonly HackingProjekt.Data.HackingProjektDBContext _context;

        public DetailsModel(HackingProjekt.Data.HackingProjektDBContext context)
        {
            _context = context;
        }

      public movie Movie { get; set; } = default!;

      

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
