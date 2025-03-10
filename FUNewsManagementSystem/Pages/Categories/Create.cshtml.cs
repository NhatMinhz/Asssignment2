using BusinessObjects;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUNewsManagementSystem.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; } = new Category();

        public List<SelectListItem> ParentCategories { get; set; } = new List<SelectListItem>();

        public string SuccessMessage { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            ParentCategories = (await _categoryService.GetAllCategoriesAsync())
                .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _categoryService.AddCategoryAsync(Category);
            SuccessMessage = "Category created successfully.";
            return RedirectToPage("Index");
        }
    }
}
