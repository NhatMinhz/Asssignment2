using FUNewsManagementSystem.DAL.Models;
using FUNewsManagementSystem.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DetailsModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
                return NotFound();

            Category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (Category == null)
                return NotFound();

            return Page();
        }
    }
}
