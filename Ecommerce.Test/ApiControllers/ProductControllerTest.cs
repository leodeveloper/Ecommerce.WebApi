using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Ecommerce.Model.Dto;
using Ecommerce.Service.Interface;
using Ecommerce.WebApi.Controllers;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Ecommerce.Test.ApiControllers
{
    public class ProductControllerTest
    {
        private ProductController _productController;
        private Mock<IProductService> _iProductServiceMock = new Mock<IProductService>();

        public ProductControllerTest()
        {
            _productController = new ProductController(_iProductServiceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsProductList()
        {
            // Arrange     
            var productDbSetMock = Builder<Product>.CreateListOfSize(5).Build();
            _iProductServiceMock.Setup(m => m.GetProductsAsync()).Returns(Task.FromResult(productDbSetMock));

            // Act
            var result = await _productController.Get();

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as IList<Product>;
            Assert.NotNull(content);

            Assert.Equal(5, content.Count);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            // Arrange
            _iProductServiceMock.Setup(m => m.GetProductAsync(1)).Returns(Task.FromResult<Product>(null));

            // Act
            var result = await _productController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }

        [Fact]
        public async Task Get_ReturnsItem()
        {
            // Arrange
            _iProductServiceMock.Setup(m => m.GetProductAsync(1)).Returns(Task.FromResult<Product>(Builder<Product>.CreateNew().Build()));

            // Act
            var result = await _productController.Get(1);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as Product;
            Assert.NotNull(content);

            Assert.Equal(1, content.Id);
        }
    }
}
