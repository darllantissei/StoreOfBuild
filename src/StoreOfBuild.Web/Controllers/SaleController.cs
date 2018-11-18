using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreOfBuild.Domain;
using StoreOfBuild.Domain.Products;
using StoreOfBuild.Domain.Sales;
using StoreOfBuild.Web.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreOfBuild.Web.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly SaleFactory _saleFactory;
        private IRepository<Product> __productRepository;

        public SaleController(SaleFactory saleFactory, IRepository<Product> productRepository)
        {
            _saleFactory = saleFactory;
            __productRepository = productRepository;
        }

        public IActionResult Create()
        {
            var product = __productRepository.All();

            if (product.Any())
            {
                var productViewModel = product.Select(c => new ProductViewModel { Id = c.Id, Name = c.Name });
                return View(new SaleViewModel { Products = productViewModel });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(SaleViewModel viewModel)
        {
            _saleFactory.Create(viewModel.ClientName, viewModel.ProductId, viewModel.Quantity);
            return Ok();
        }



    }
}
