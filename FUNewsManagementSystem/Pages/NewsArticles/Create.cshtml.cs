using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Service.Interfaces;
using Microsoft.AspNetCore.SignalR;
using FUNewsManagementSystem.Hubs;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly INewArticleService _newArticleService;
        private readonly ISystemAccountService _systemAccountService;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(ICategoryService categoryService,
            ITagService tagService,
            INewArticleService newArticleService,
            ISystemAccountService systemAccountService,
            IHubContext<SignalRServer> hubContext)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _newArticleService = newArticleService;
            _systemAccountService = systemAccountService;
            _hubContext = hubContext;
        }

        public SelectList Categories { get; set; }
        public SelectList SystemAccounts { get; set; }
        public SelectList TagIds { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryDesciption");
            SystemAccounts = new SelectList(await _systemAccountService.GetAllAccountsAsync(), "AccountId", "AccountId");
            TagIds = new SelectList(_tagService.GetAllTags(), "TagId", "TagName");
            return Page();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;
        [BindProperty]
        public List<int> ListTag { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ListTag == null || !ListTag.Any())
            {
                ModelState.AddModelError("ListTag","Bạn phải chọn ít nhất 1 tag");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                return new JsonResult(new { success = false, errors });
            }

            await _newArticleService.AddNewsArticleAsync(NewsArticle, ListTag);
            await _hubContext.Clients.All.SendAsync("LoadNewsArticlesItem");
            return RedirectToPage("./Index");
        }
    }
}
