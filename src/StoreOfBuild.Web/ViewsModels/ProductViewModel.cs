using StoreOfBuild.Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreOfBuild.Web.ViewsModels
{
    public class ProductViewModel
    {
        public int Id { get;  set; }

        [Required]
        public string Name { get;  set; }

        public string CategoryName { get;  set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Required]
        public decimal Price { get;  set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Stock Quantity invalid")]
        public int StockQuantity { get;  set; }
    }
}
