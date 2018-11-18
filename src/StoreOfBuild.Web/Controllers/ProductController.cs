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
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductStorer _productStorer;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        /// <summary>
        /// Aqui no método construtor está vindo a CategoryStorer via Injeção de Dependência
        /// </summary>
        /// <param name="productStorer"></param>
        /// <param name="productRepository"></param>
        public ProductController(ProductStorer productStorer, IRepository<Category> categoryRepository, IRepository<Product> productRepository)
        {
            _productStorer = productStorer;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.All();
            if (products.Any())
            {
                var viewsModels = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category?.Name,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity
                });
                return View(viewsModels);
            }
            return Redirect("/Product/CreateOrEdit");
        }

        public IActionResult CreateOrEdit(int id)
        {
            var viewModel = new ProductViewModel();
            var categories = _categoryRepository.All();
            viewModel.Categories = categories.Any() ?
                categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name })
                : new List<CategoryViewModel>();

            var product = _productRepository.GetById(id);
            if (product == null)
                return View(viewModel);
            else
            {
                viewModel.Id = product.Id;
                viewModel.Name = product.Name;
                viewModel.CategoryId = product.Category.Id;
                viewModel.Price = product.Price;
                viewModel.StockQuantity = product.StockQuantity;
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult CreateOrEdit(ProductViewModel viewModelo)
        {
            _productStorer.Store(viewModelo.Id, viewModelo.Name, viewModelo.CategoryId, viewModelo.Price, viewModelo.StockQuantity);
            return Redirect("Index");
        }      
    }
}
