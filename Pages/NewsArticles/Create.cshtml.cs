using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Service.Interfaces;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly INewArticleService _newArticleService;
        private readonly ISystemAccountService _systemAccountService;

        public CreateModel(ICategoryService categoryService, ITagService tagService, INewArticleService newArticleService, ISystemAccountService systemAccountService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _newArticleService = newArticleService;
            _systemAccountService = systemAccountService;
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _newArticleService.AddNewsArticleAsync(NewsArticle, ListTag);
            return RedirectToPage("./Index");
        }
    }
}
