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
    public class DetailsModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public DetailsModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

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
    }
}
