using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HackingProjekt.modelLib;
using HackingProjekt.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);




/*
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://Google.com"
                                              );
                      });
});
*/


builder.Services.AddRazorPages();

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{  
})
    .AddEntityFrameworkStores<HackingProjektDBContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    
    
    ; ;

builder.Services.AddAuthorization(options => 
{   //Rettigheder 
   
   options.AddPolicy("EditPolicy", policy =>
    {
        policy.RequireAuthenticatedUser(); // Bruger skal være logget ind 
        policy.RequireClaim("MovieAuthor"); // Bruger skal Havde dette claim 
    })
    
    ;
    
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        /*
        options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
        options.SlidingExpiration = false;
        */
       
    });


builder.Services.Configure<IdentityOptions>(options =>
{
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 2;
    options.Lockout.AllowedForNewUsers = true;



    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;


   
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AcceseDenied"; 

});




builder.Services.AddDbContext<HackingProjektDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HackingProjektContext") ?? throw new InvalidOperationException("Connection string 'HackingProjektContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

 
}


// Setting up roles
/*
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>> ();

    var Roles = new[]{ "Admin", "User" };

    foreach (var role in Roles)
    {
        if( await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));

        }
    }
        
}
*/



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();






app.Run();
