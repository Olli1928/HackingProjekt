using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HackingProjekt.Data;
using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Authorization;

namespace HackingProjekt.Pages.MoviesScafold
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly HackingProjekt.Data.HackingProjektDBContext _context;

        public EditModel(HackingProjekt.Data.HackingProjektDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public movie Movie { get; set; } = default!;

        public List<string> AldersMærker { get; } = new List<string> { "A", "7", "11", "15" };

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie =  await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
