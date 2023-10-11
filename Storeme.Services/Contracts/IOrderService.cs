using Storeme.Entities;

namespace Storeme.Services.Contracts
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(string username);

        Task<List<Order>> GetAllOrders();

        Task<List<Order>> UserOrders(string username);

        Task<MemoryStream> ExportOrders(string username, List<Order> orders);

        Task<StoremeUser> GetUser(string username);
    }
}
