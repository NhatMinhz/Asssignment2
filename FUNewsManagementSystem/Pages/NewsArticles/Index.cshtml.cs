using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interfaces;
using Service.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNewsManagementSystem.Pages.NewsArticles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly INewArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ISystemAccountService _systemAccountService;
        private readonly ITagService _tagService;

        public IndexModel(INewArticleService newsArticleService, ICategoryService categoryService, ISystemAccountService systemAccountService,ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _systemAccountService = systemAccountService;
            _tagService = tagService;
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; }
        public List<NewsArticle> NewsArticles { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<int> ListTag { get; set; } = new();

        public SelectList CategoriesSelectList { get; set; }
        public SelectList SystemAccounts { get; set; }
        public SelectList TagIds { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int? CategoryFilter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            var articles = await _newsArticleService.GetFilteredNewsArticlesAsync(SearchTitle, CategoryFilter);
            NewsArticles = articles.ToList();

            CategoriesSelectList = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryDesciption");
            SystemAccounts = new SelectList(await _systemAccountService.GetAllAccountsAsync(), "AccountId", "AccountId");
            TagIds = new SelectList(_tagService.GetAllTags(), "TagId", "TagName");
            return Page();
        }
    }
}
