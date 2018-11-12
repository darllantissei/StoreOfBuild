
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
        public void Store(int id, string name, int categoryId, decimal price, int stockQuantity)
        {
            var category = _categoreRepository.GetById(categoryId);
            DomainException.When(category == null, "Category invalid");

            var product = _productRepository.GetById(id);
            if (product == null)
            {
                product = new Product(name, category, price, stockQuantity);
                _productRepository.Save(product);
            }
            else
            {
                product.Update(name, category, price, stockQuantity);
            }
        }
    }
}