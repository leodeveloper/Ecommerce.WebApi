using Ecommerce.Model.Dto;
using Ecommerce.Model.GenericRepository.Repository;
using Ecommerce.Service.Interface;
using Ecommerce.Service.Service;
using FizzWare.NBuilder;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Test.Service
{
    public class ProductServiceTest
    {
        private IProductService _iProductService;
        private Mock<IRepositoryReadOnly> _iRepositoryMock = new Mock<IRepositoryReadOnly>();
        IEnumerable<Product> _productsDbSetMock;
        Product _productDbSetMock;

        public ProductServiceTest()
        {
            _iProductService = new ProductService(_iRepositoryMock.Object);
            _productsDbSetMock = Builder<Product>.CreateListOfSize(5).Build().AsEnumerable();
            _productDbSetMock = Builder<Product>.CreateNew().Build();
        }

        [Fact]
        public async Task Get_AllProduct_ReturnsProductList()
        {
            // Arrange     
            _iRepositoryMock.Setup(m => m.GetAllAsync<Product>(null, null, null, null))
                .Returns(Task.FromResult(_productsDbSetMock));

            // Act
            var result = await _iProductService.GetProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

        }

        [Fact]
        public async Task Get_Product_ReturnsProduct()
        {
            // Arrange 
            _iRepositoryMock.Setup(moq => moq.GetOneAsync(It.IsAny<Expression<Func<Product, bool>>>(),null)).Returns(Task.FromResult(_productDbSetMock));
            
            // Act
            var result = await _iProductService.GetProductAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
