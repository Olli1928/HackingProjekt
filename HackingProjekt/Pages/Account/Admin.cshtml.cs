using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackingProjekt.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HackingProjekt.Pages.Account
{
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminModel(UserManager<IdentityUser> context)
        {
            _userManager = context;
        }

        public new IList<IdentityUser> Users { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }


        
        public async Task OnGetAsync()
        {
            var movies = from u in _userManager.Users
                         select u;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.UserName.Contains(SearchString));
            }

            Users = await movies.ToListAsync();
        }
    }
}

