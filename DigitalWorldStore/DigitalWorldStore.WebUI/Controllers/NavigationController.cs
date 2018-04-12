using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalWorldStore.Domain.Abstract_Repository;

namespace DigitalWorldStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {

        private ICategoryRepository categoryRepository;

        public NavigationController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public PartialViewResult CategoriesList(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = categoryRepository.Categories
                                .Select(c => c.CategoryName)
                                .Distinct()
                                .OrderBy(c => c);
            return PartialView(categories);
        }
    }
}