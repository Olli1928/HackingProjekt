using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HackingProjekt.Data;
using HackingProjekt.modelLib;
using Microsoft.AspNetCore.Authorization;

namespace HackingProjekt.Pages.MoviesScafold
{
    [ValidateAntiForgeryToken]
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly HackingProjekt.Data.HackingProjektDBContext _context;

        public CreateModel(HackingProjekt.Data.HackingProjektDBContext context)
        {
            _context = context;

            
        }

        [BindProperty]
        public movie Movie { get; set; } = default!;

        public List<string> AldersMærker { get; } = new List<string> { "A", "7", "11", "15" };




        public IActionResult OnGet()
        {
           
            return Page();
        }

       
        

        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Movie == null || Movie == null)
            {
                return Page();
            }

            //Movie.Forfatter = User.Identity.Name;
            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
