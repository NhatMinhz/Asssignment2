using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.SystemAccounts
{
    public class DeleteModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public DeleteModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount = await _systemAccountService.GetAccountByIdAsync(id);

            if (systemaccount == null)
            {
                return NotFound();
            }
            else
            {
                SystemAccount = systemaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _systemAccountService.DeleteAccountAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
