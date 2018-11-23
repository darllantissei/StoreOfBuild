using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreOfBuild.Domain;
using StoreOfBuild.Domain.Products;
using StoreOfBuild.Web.Models;
using StoreOfBuild.Web.ViewsModels;

namespace StoreOfBuild.Web.Controllers
{
    [Authorize(Roles ="Admin, Manager")]
    public class CategoryController : Controller
    {
        private readonly CategoryStorer _categoryStorer;
        private readonly IRepository<Category> _categoryRepository;

        /// <summary>
        /// Aqui no método construtor está vindo a CategoryStorer via Injeção de Dependência
        /// </summary>
        /// <param name="categoryStorer"></param>
        /// <param name="categoryRepository"></param>
        public CategoryController(CategoryStorer categoryStorer, IRepository<Category> categoryRepository)
        {
            _categoryStorer = categoryStorer;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.All();

            var viewsModels = categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name });
            return View(viewsModels);
        }

        public IActionResult CreateOrEdit(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null)
                return View();
            else
            {
                var categoryviewModel = new CategoryViewModel { Id = category.Id, Name = category.Name };

                return View(categoryviewModel);
            }
        }

        [HttpPost]
        public IActionResult CreateOrEdit(CategoryViewModel viewModelo)
        {
            _categoryStorer.Store(viewModelo.Id, viewModelo.Name);
            return Redirect("/Category/Index");// View();
        }      
    }
}
