using StoreOfBuild.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreOfBuild.Domain.Sales
{
    public class Sale : Entity
    {
        public string ClientName { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public decimal Total { get; private set; }
        public SaleItem Item { get; private set; }

        public Sale() { }

        public Sale(string clientName, Product product, int quantity)
        {
            DomainException.When(string.IsNullOrEmpty(clientName), "Client name is required");
            this.Item = new SaleItem(product, quantity);
            this.CreatedOn = DateTime.Now;
            this.ClientName = clientName;
        }
    }
}
