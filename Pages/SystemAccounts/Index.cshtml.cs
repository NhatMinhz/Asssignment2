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
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountService _systemAccountService;

        public IndexModel(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        public IList<SystemAccount> SystemAccounts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IEnumerable<SystemAccount> listSystemAccount = await _systemAccountService.GetAllAccountsAsync();
            SystemAccounts = listSystemAccount.ToList();
        }
    }
}
