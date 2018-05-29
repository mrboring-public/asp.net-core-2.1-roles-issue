using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public bool IsAdmin { get; private set; }
        public IList<string> Roles { get; private set; }
        public string Message { get; set; }
        public IServiceProvider ServiceProvider { get; }

        public async Task OnGet()
        {
            if (User.Identity.Name != null)
            {
                var userManager = ServiceProvider.GetService<UserManager<IdentityUser>>();
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                Roles = await userManager.GetRolesAsync(user);
            }

            IsAdmin = User.IsInRole("Admin");

            Message = "Your contact page.";
        }
    }
}
