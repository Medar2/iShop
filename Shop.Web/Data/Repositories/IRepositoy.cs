
namespace Shop.Web.Data
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IRepositoy
    {
        void AddProducts(Product product);
        
        Product GetProduct(int id);
        
        IEnumerable<Product> GetProducts();
        
        bool ProductExists(int id);
        
        void RemoveProducts(Product product);
        
        Task<bool> SaveAllAsync();
        void UpdateProducts(Product product);
    }
}