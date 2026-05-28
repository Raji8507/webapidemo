using webapidemo.Models;

namespace webapidemo.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> _products = new()
        {
            new Product
            {
                ProductId = 101,
                ProductName = "Laptop",
                ProductDescription = "Dell Laptop",
                ProductPrice = 90000,
                CatId = 1,
                IsAvailable = true
            },

            new Product
            {
                ProductId = 102,
                ProductName = "Mobile",
                ProductDescription = "Samsung Mobile",
                ProductPrice = 25000,
                CatId = 1,
                IsAvailable = true
            },

            new Product
            {
                ProductId = 103,
                ProductName = "Headphones",
                ProductDescription = "Boat Headphones",
                ProductPrice = 2000,
                CatId = 2,
                IsAvailable = true
            }
        };

        public IEnumerable<Product> GetAllProduct()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products
                .FirstOrDefault(
                    p => p.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            product.ProductId =
                _products.Max(
                    p => p.ProductId) + 1;

            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existing =
                _products.FirstOrDefault(
                    p => p.ProductId ==
                    product.ProductId);

            if (existing != null)
            {
                existing.ProductName =
                    product.ProductName;

                existing.ProductDescription =
                    product.ProductDescription;

                existing.ProductPrice =
                    product.ProductPrice;

                existing.CatId =
                    product.CatId;

                existing.IsAvailable =
                    product.IsAvailable;
            }
        }

        public void DeleteProduct(int id)
        {
            var product =
                _products.FirstOrDefault(
                    p => p.ProductId == id);

            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}