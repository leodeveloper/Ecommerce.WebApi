using Ecommerce.Model.Dto;
using Ecommerce.Model.GenericRepository.Repository;
using Ecommerce.Service.Interface;
using Ecommerce.Service.Service;
using FizzWare.NBuilder;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq.Expressions;

namespace Ecommerce.Test.Service
{
    public class BasketServiceTest
    {
        private IBasketService _iBasketService;
        private Mock<IRepository> _iRepositoryMock = new Mock<IRepository>();
        IEnumerable<BasketItem> basketItemsDbSetMock;
        BasketItem basketItemDbSetMock;

        public BasketServiceTest()
        {
            _iBasketService = new BasketService(_iRepositoryMock.Object);
            basketItemsDbSetMock = Builder<BasketItem>.CreateListOfSize(5).All().With(b=>b.UserId = 1).Build().AsEnumerable();
            basketItemDbSetMock = Builder<BasketItem>.CreateNew().Build();
        }

        [Fact]
        public async Task Get_AllBasketItem_ReturnsBasketItem()
        {
            // Arrange     
            _iRepositoryMock.Setup(m => m.GetAsync<BasketItem>(null, null,null, null, null))
                .Returns(Task.FromResult(basketItemsDbSetMock));

            // Act
            var result = await _iBasketService.GetBasketItemsAsync(1);

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task AddItemIntoBasket_ReturnsBasketItem()
        {
            // Arrange     
            _iRepositoryMock.Setup(m => m.GetAsync<BasketItem>(b => b.UserId == 1, null, null, null, null)).Returns(Task.FromResult(basketItemsDbSetMock));

            // Act
            var result = await _iBasketService.AddItemintoBasketAsync(basketItemDbSetMock);

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(6, result.Id);
        }

        [Fact]
        public async Task ClearAllBasketItem()
        {
            // Arrange   
           // _iRepositoryMock.Setup(m => m.GetAllAsync<BasketItem>(null, null, null, null)).Returns(Task.FromResult(basketItemsDbSetMock));
            _iRepositoryMock.Setup(m => m.GetAsync<BasketItem>(b => b.UserId == 1, null, null, null, null)).Returns(Task.FromResult(basketItemsDbSetMock.Where(b=>b.UserId==1)));

            // Act
            var result = await _iBasketService.ClearBasketAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task DeleteBasketItem_ById()
        {
            // Arrange   
            _iRepositoryMock.Setup(m => m.GetAllAsync<BasketItem>(null, null, null, null)).Returns(Task.FromResult(basketItemsDbSetMock));
            _iRepositoryMock.Setup(m => m.GetByIdAsync<BasketItem>(1)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _iBasketService.DeleteBasketItemByIdAsync(1);

            // Assert
            Assert.NotNull(result);

        }      

        [Fact]
        public async Task Change_Basket_Item_Quanity()
        {
            // Arrange   
            _iRepositoryMock.Setup(m => m.GetAllAsync<BasketItem>(null, null, null, null)).Returns(Task.FromResult(basketItemsDbSetMock));
            _iRepositoryMock.Setup(m => m.GetByIdAsync<BasketItem>(1)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _iBasketService.ChangeBasketItemQuantityAsync(1, 4);

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task BasketItem_Not_Found_when_Change_Quantity()
        {
            // Arrange   
            _iRepositoryMock.Setup(m => m.GetAllAsync<BasketItem>(null, null, null, null)).Returns(Task.FromResult(basketItemsDbSetMock));
            _iRepositoryMock.Setup(m => m.GetByIdAsync<BasketItem>(1)).Returns(Task.FromResult(basketItemDbSetMock));

            // Act
            var result = await _iBasketService.ChangeBasketItemQuantityAsync(8, 4);

            // Assert
            Assert.Null(result);

        }
    }
}
