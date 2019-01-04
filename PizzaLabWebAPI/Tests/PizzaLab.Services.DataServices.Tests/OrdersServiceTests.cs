namespace PizzaLab.Services.DataServices.Tests
{    
    using AutoMapper;    
    using Data;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models.Orders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Helpers;
    using Xunit;

    public class OrdersServiceTests
    {
        private readonly IRepository<Order> _ordersRepository;
        private readonly OrdersService _ordersService;

        public OrdersServiceTests()
        {
            var options = new DbContextOptionsBuilder<PizzaLabDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PizzaLabDbContext(options);

            var mapperProfile = new MappingConfiguration();
            var conf = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            var mapper = new Mapper(conf);

            this._ordersRepository = new EfRepository<Order>(dbContext);
            var orderProductRepository = new EfRepository<OrderProduct>(dbContext);
            this._ordersService = new OrdersService(_ordersRepository, orderProductRepository, mapper);
        }

        [Fact]
        public async Task CreateOrderAsyncShouldCreateOrderSuccessfully()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            var createdOrderDto = await _ordersService.CreateOrderAsync("userID", orderProducts);

            Assert.Equal("userID", createdOrderDto.CreatorId);
            Assert.Equal(OrderStatus.Pending.ToString(), createdOrderDto.Status);
            Assert.Equal(2, createdOrderDto.OrderProducts.Count());

            var firstOrderProduct = createdOrderDto.OrderProducts.First();
            Assert.Equal("1234", firstOrderProduct.Id);
            Assert.Equal(9.90m, firstOrderProduct.Price);
            Assert.Equal(1, firstOrderProduct.Quantity);

            var secondOrderProduct = createdOrderDto.OrderProducts.Last();
            Assert.Equal("2345", secondOrderProduct.Id);
            Assert.Equal(10.90m, secondOrderProduct.Price);
            Assert.Equal(2, secondOrderProduct.Quantity);
        }

        [Fact]
        public async Task ExistsShouldReturnTrue()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);
            var orderId = _ordersRepository.All().First().Id;

            Assert.True(_ordersService.Exists(orderId));
        }

        [Fact]
        public void ExistsShouldReturnFalse()
        {
            Assert.False(_ordersService.Exists("orderId"));
        }

        [Fact]
        public async Task ApproveOrderAsyncShouldApproveOrderSuccessfully()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);
            var orderId = _ordersRepository.All().First().Id;

            await _ordersService.ApproveOrderAsync(orderId);

            Assert.Equal(OrderStatus.Approved, _ordersRepository.All().First().Status);
        }

        [Fact]
        public async Task DeleteProductOrdersAsyncShouldDeleteProductOrdersSuccessfully()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);

            await _ordersService.DeleteProductOrdersAsync("2345");
            var order = _ordersRepository.All().First();

            Assert.Equal(1, order.Products.Count);
        }

        [Fact]
        public async Task GetUserOrdersShouldReturnUserOrdersCorrectly()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);

            var userOrders = _ordersService.GetUserOrders("userID").ToList();

            Assert.Single(userOrders);
        }

        [Fact]
        public async Task GetUserOrdersShouldReturnEmptyCollection()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);

            var userOrders = _ordersService.GetUserOrders("user").ToList();

            Assert.Empty(userOrders);
        }

        [Fact]
        public async Task GetPendingOrdersShouldReturnPendingOrdersCorrectly()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);
            await _ordersService.CreateOrderAsync("user", orderProducts);

            var pendingOrders = _ordersService.GetPendingOrders();

            Assert.Equal(2, pendingOrders.Count());
        }

        [Fact]
        public void GetPendingOrdersShouldReturnEmptyCollection()
        {
            var pendingOrders = _ordersService.GetPendingOrders();

            Assert.Empty(pendingOrders);
        }

        [Fact]
        public async Task GetApprovedOrdersShouldReturnApprovedOrdersCorrectly()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);
            var firstOrderId = _ordersRepository.All().First().Id;

            await _ordersService.CreateOrderAsync("user", orderProducts);
            var secondOrderId = _ordersRepository.All().Last().Id;

            await _ordersService.ApproveOrderAsync(firstOrderId);
            await _ordersService.ApproveOrderAsync(secondOrderId);

            var approvedOrders = _ordersService.GetApprovedOrders();

            Assert.Equal(2, approvedOrders.Count());
        }

        [Fact]
        public async Task GetApprovedOrdersShouldReturnEmptyCollection()
        {
            var orderProducts = new List<OrderProductDto>
            {
                new OrderProductDto
                {
                    Id = "1234",
                    Name = "Diablo",
                    Price = 9.90m,
                    Quantity = 1
                },
                new OrderProductDto
                {
                    Id = "2345",
                    Name = "Pollo",
                    Price = 10.90m,
                    Quantity = 2
                }
            };

            await _ordersService.CreateOrderAsync("userID", orderProducts);
            await _ordersService.CreateOrderAsync("user", orderProducts);

            var approvedOrders = _ordersService.GetApprovedOrders();

            Assert.Empty(approvedOrders);
        }
    }
}
