using Microsoft.AspNetCore.Mvc;
using ProgrammerBlog.Services.Services.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            return View(result.Data);
        }

        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
    }
}
