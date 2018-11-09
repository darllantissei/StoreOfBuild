using StoreOfBuild.Domain.Dtos;

namespace StoreOfBuild.Domain.Products
{
    public class ProductStorer
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoreRepository;

        public ProductStorer(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoreRepository = categoryRepository;
        }
        public void Store(ProductDto dto)
        {
            var category = _categoreRepository.GetById(dto.CategoryId);
            DomainException.When(category == null, "Category invalid");

            var product = _productRepository.GetById(dto.Id);
            if (product == null)
            {
                product = new Product(dto.Name, category, dto.Price, dto.StockQuantity);
                _productRepository.Save(product);
            }
            else
            {
                product.Update(dto.Name, category, dto.Price, dto.StockQuantity);
            }
        }
    }
}