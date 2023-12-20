using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HackingProjekt.Data;
using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Authorization;

namespace HackingProjekt.Pages.MoviesScafold
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly HackingProjekt.Data.HackingProjektDBContext _context;

        public DeleteModel(HackingProjekt.Data.HackingProjektDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public movie Movie{ get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);

            if (movie != null)
            {
                Movie = movie;
                _context.Movie.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
