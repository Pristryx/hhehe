using ASAssignment212344H.Model;
using ASAssignment212344H.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ASAssignment212344H.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor contxt;
        private UserManager<ApplicationUser> _userManager;
        public IndexModel(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            contxt = httpContextAccessor;
            _userManager = userManager;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }

        public void OnGet()
        {
            if (contxt.HttpContext.Request.Cookies.ContainsKey("logged-in-browser"))
            {
                var userId = contxt.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = _userManager.FindByIdAsync(userId).Result;
                if(user != null)
                {
                    Email = user.Email;
                    FirstName = user.FirstName;

                    contxt.HttpContext.Session.SetString("Email", Email);
                    contxt.HttpContext.Session.SetString("FirstName", FirstName);
                }
                

            }

        }
    }
}