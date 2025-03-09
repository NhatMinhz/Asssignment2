using BusinessObjects;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interfaces;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Pages.SystemAccounts
{
    public class DetailsModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public DetailsModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        public SystemAccount? SystemAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(short id)
        {
            if (id == 0) 
            {
                return NotFound();
            }

            SystemAccount = await _systemAccountService.GetAccountByIdAsync(id);

            if (SystemAccount == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
