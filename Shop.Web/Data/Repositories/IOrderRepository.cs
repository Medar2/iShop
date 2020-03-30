using Shop.Web.Data.Entities;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Repositories
{
    public interface IOrderRepository: IGenericRepository<Order>
    {

        Task AddItemToOrderAsync(AddItemViewModel model, string userName);
        Task<IQueryable<Order>> GetOrdersAsync(string userName);
        Task<Order> GetOrdersAsync(int id);

        Task<IQueryable<OrderDetailTemp>> GetOrderDetailTempAsync(string userName);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        Task DeleteDetailTempAsync(int id);

        Task<bool> ConfirmOrderAsync(string userName);

        Task DeliverOrder(DeliverViewModel model);

    }
}
