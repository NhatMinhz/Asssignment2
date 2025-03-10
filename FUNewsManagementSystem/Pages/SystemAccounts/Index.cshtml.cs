using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
