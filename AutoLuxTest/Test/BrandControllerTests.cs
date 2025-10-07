using NUnit.Framework;
using Moq;
using AutoMapper;
using AutoLuxBackend.Controllers;
using AutoLuxBackend.DTO.BrandDTO;
using AutoLuxBackend.Models;
using AutoLuxBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoLuxTest
{
    [TestFixture]
    public class BrandControllerTests
    {
        private Mock<IBrandRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;
        private BrandController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IBrandRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new BrandController(_mockRepo.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldReturnOk_WhenBrandCreated()
        {
            // Arrange
            var createDto = new BrandCreateDTO { Name = "MG" };
            var domainBrand = new Brand { Id = 11, Name = "MG" };
            var viewDto = new BrandViewDTO { Id = 11, Name = "MG" };

            _mockRepo.Setup(r => r.GetByNameAsync(createDto.Name)).ReturnsAsync((Brand)null);
            _mockMapper.Setup(m => m.Map<Brand>(createDto)).Returns(domainBrand);
            _mockRepo.Setup(r => r.AddAsync(domainBrand)).ReturnsAsync(domainBrand);
            _mockMapper.Setup(m => m.Map<BrandViewDTO>(domainBrand)).Returns(viewDto);

            // Act
            var res = await _controller.CreateAsync(createDto);

            // Assert
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(viewDto));
        }

        [Test]
        public async Task CreateAsync_ShouldReturnBadRequest_IfBrandExists()
        {
            var createDto = new BrandCreateDTO { Name = "MG" };
            var existingBrand = new Brand { Id = 99, Name = "MG" };
            _mockRepo.Setup(r => r.GetByNameAsync(createDto.Name)).ReturnsAsync(existingBrand);

            var res = await _controller.CreateAsync(createDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(res);
            var result = res as BadRequestObjectResult;
            Assert.That(result.Value, Is.EqualTo("Brand already exists!"));
        }

        [Test]
        public async Task GetAllBrandsAsync_ShouldReturnOkWithBrandList()
        {
            var domainList = new List<Brand>
            {
                new Brand{Id=1, Name="A"}, new Brand{Id=2, Name="B"}
            };
            var viewList = new List<BrandViewDTO>
            {
                new BrandViewDTO { Id = 1, Name = "A" },
                new BrandViewDTO { Id = 2, Name = "B" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(domainList);
            _mockMapper.Setup(m => m.Map<BrandViewDTO>(domainList[0])).Returns(viewList[0]);
            _mockMapper.Setup(m => m.Map<BrandViewDTO>(domainList[1])).Returns(viewList[1]);

            var res = await _controller.GetAllBrandsAsync();

            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            var valueList = okRes.Value as List<BrandViewDTO>;
            Assert.IsNotNull(valueList);
            Assert.That(valueList.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnOk_WhenFound()
        {
            var id = 5;
            var domainBrand = new Brand { Id = id, Name = "Nissan" };
            var viewBrand = new BrandViewDTO { Id = id, Name = "Nissan" };

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(domainBrand);
            _mockMapper.Setup(m => m.Map<BrandViewDTO>(domainBrand)).Returns(viewBrand);

            var res = await _controller.GetByIdAsync(id);
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(viewBrand));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNotFound_WhenNotFound()
        {
            var id = 5555;
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Brand)null);

            var res = await _controller.GetByIdAsync(id);

            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnOk_WhenBrandUpdated()
        {
            var id = 7;
            var existingBrand = new Brand { Id = id, Name = "Old" };
            var updateDto = new BrandCreateDTO { Name = "NewName" };
            var updatedView = new BrandViewDTO { Id = id, Name = "NewName" };

            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingBrand);
            _mockRepo.Setup(r => r.UpdateAsync(existingBrand));
            _mockMapper.Setup(m => m.Map<BrandViewDTO>(existingBrand)).Returns(updatedView);

            var res = await _controller.UpdateAsync(id, updateDto);
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(updatedView));
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnNotFound_IfMissing()
        {
            var id = 888;
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Brand)null);

            var res = await _controller.UpdateAsync(id, new BrandCreateDTO { Name = "Any" });

            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnOk_WhenDeleted()
        {
            _mockRepo.Setup(r => r.DeleteAsync(777)).ReturnsAsync(true);

            var res = await _controller.DeleteAsync(777);

            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnNotFound_WhenNotDeleted()
        {
            _mockRepo.Setup(r => r.DeleteAsync(842)).ReturnsAsync(false);

            var res = await _controller.DeleteAsync(842);

            Assert.IsInstanceOf<NotFoundObjectResult>(res);
        }
    }
}
