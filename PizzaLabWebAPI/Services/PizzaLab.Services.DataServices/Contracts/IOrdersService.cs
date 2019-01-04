namespace PizzaLab.Services.DataServices.Contracts
{
    using Models.Orders;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task ApproveOrderAsync(string orderId);

        Task<OrderDto> CreateOrderAsync(string userId, IEnumerable<OrderProductDto> orderProducts);

        bool Exists(string orderId);

        IEnumerable<OrderDto> GetApprovedOrders();

        IEnumerable<OrderDto> GetPendingOrders();

        IEnumerable<OrderDto> GetUserOrders(string userId);

        Task DeleteProductOrdersAsync(string productId);
    }
}
