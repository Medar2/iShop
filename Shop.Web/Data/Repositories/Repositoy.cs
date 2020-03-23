using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
    public class Repositoy : IRepositoy
    {
        private readonly DataContext context;

        public Repositoy(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.context.Products.OrderBy(p => p.Name);
        }

        public Product GetProduct(int id)
        {
            return this.context.Products.Find(id);
        }

        public void AddProducts(Product product)
        {
            this.context.Products.Add(product);
        }
        public void UpdateProducts(Product product)
        {
            //this.context.Update(product); //Es valido
            this.context.Products.Update(product);
        }
        public void RemoveProducts(Product product)
        {
            this.context.Products.Remove(product);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
        public bool ProductExists(int id)
        {
            return this.context.Products.Any(p => p.Id == id);
        }


    }
}
