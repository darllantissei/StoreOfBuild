using StoreOfBuild.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreOfBuild.Domain.Sales
{
    public class SaleItem : Entity
    {
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public SaleItem() { }

        public SaleItem(Product product, int quantity)
        {
            DomainException.When(product == null, "Product is required");
            DomainException.When(quantity < 1, "Quantity incorrect");

            this.Product = product;
            this.Price = Product.Price;
            this.Quantity = quantity;
            this.Total = this.Price * this.Quantity;

        }
    }
}
