using StoreOfBuild.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreOfBuild.Domain.Sales
{
    public class SaleFactory
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Product> _productRepository;

        public SaleFactory(IRepository<Sale> saleRepository, IRepository<Product> productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public void Create(string clientName, int productId, int quantity)
        {
            var product = _productRepository.GetById(productId);
            product.RemoveFromStock(quantity);

            var sale = new Sale(clientName, product, quantity);
            _saleRepository.Save(sale);
        }
    }
}
