﻿using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                categories = categories
                    .Where(c => c.CategoryName.Contains(SearchString, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Categories = (List<Category>)categories;
        }
    }
}
