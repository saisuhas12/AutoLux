using NUnit.Framework;
using Moq;
using AutoMapper;
using AutoLuxBackend.Controllers;
using AutoLuxBackend.DTO.CarDTO;
using AutoLuxBackend.Models;
using AutoLuxBackend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoLuxTest
{
    [TestFixture]
    public class CarControllerTests
    {
        private Mock<ICarRepository> _mockCarRepo;
        private Mock<IBrandRepository> _mockBrandRepo;
        private Mock<IMapper> _mockMapper;
        private CarController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCarRepo = new Mock<ICarRepository>();
            _mockBrandRepo = new Mock<IBrandRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CarController(_mockCarRepo.Object, _mockBrandRepo.Object, _mockMapper.Object);
        }

        [Test]
        public async Task AddCarAsync_ShouldReturnOk_WhenBrandIdIsValid()
        {
            var dto = new CarCreateDTO { Model = "Civic", BrandId = 5 };
            var brand = new Brand { Id = 5, Name = "Honda" };
            var mappedCar = new Cars { Id = 1, Model = "Civic", BrandId = 5, Brand = brand };
            var viewDto = new CarViewDTO { Id = 1, Model = "Civic", BrandId = 5, BrandName = "Honda" };

            _mockBrandRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(brand);
            _mockMapper.Setup(m => m.Map<Cars>(dto)).Returns(mappedCar);
            _mockCarRepo.Setup(r => r.AddAsync(mappedCar)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(mappedCar)).Returns(viewDto);

            var res = await _controller.AddCarAsync(dto);

            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(viewDto));
        }

        [Test]
        public async Task AddCarAsync_ShouldReturnOk_WhenBrandNameExists()
        {
            var dto = new CarCreateDTO { Model = "Corolla", BrandName = "Toyota" };
            var brand = new Brand { Id = 10, Name = "Toyota" };
            var mappedCar = new Cars { Id = 2, Model = "Corolla", BrandId = 10, Brand = brand };
            var viewDto = new CarViewDTO { Id = 2, Model = "Corolla", BrandId = 10, BrandName = "Toyota" };

            _mockBrandRepo.Setup(r => r.GetByNameAsync(dto.BrandName)).ReturnsAsync(brand);
            _mockMapper.Setup(m => m.Map<Cars>(dto)).Returns(mappedCar);
            _mockCarRepo.Setup(r => r.AddAsync(mappedCar)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(mappedCar)).Returns(viewDto);

            var res = await _controller.AddCarAsync(dto);

            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(viewDto));
        }

        [Test]
        public async Task AddCarAsync_ShouldAddAndReturnOk_WhenBrandNameIsNew()
        {
            var dto = new CarCreateDTO { Model = "Camry", BrandName = "Lexus" };
            Brand? nullBrand = null;
            var newBrand = new Brand { Id = 20, Name = "Lexus" };
            var mappedCar = new Cars { Id = 3, Model = "Camry", BrandId = 20, Brand = newBrand };
            var viewDto = new CarViewDTO { Id = 3, Model = "Camry", BrandId = 20, BrandName = "Lexus" };

            _mockBrandRepo.Setup(r => r.GetByNameAsync(dto.BrandName)).ReturnsAsync(nullBrand);
            _mockBrandRepo.Setup(r => r.AddAsync(It.IsAny<Brand>())).ReturnsAsync(newBrand);
            _mockMapper.Setup(m => m.Map<Cars>(dto)).Returns(mappedCar);
            _mockCarRepo.Setup(r => r.AddAsync(mappedCar)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(mappedCar)).Returns(viewDto);

            var res = await _controller.AddCarAsync(dto);

            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(viewDto));
        }

        [Test]
        public async Task AddCarAsync_ShouldReturnBadRequest_WhenNoBrandAndNoBrandId()
        {
            var dto = new CarCreateDTO { Model = "Mustang" }; // Neither brandId nor brandName given
            var res = await _controller.AddCarAsync(dto);
            Assert.IsInstanceOf<BadRequestObjectResult>(res);
            Assert.That(((BadRequestObjectResult)res).Value, Is.EqualTo("BrandId or BrandName must be provided"));
        }

        [Test]
        public async Task AddCarAsync_ShouldReturnBadRequest_WhenBrandIdInvalid()
        {
            var dto = new CarCreateDTO { Model = "Figo", BrandId = 99 };
            _mockBrandRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Brand)null);

            var res = await _controller.AddCarAsync(dto);

            Assert.IsInstanceOf<BadRequestObjectResult>(res);
            Assert.That(((BadRequestObjectResult)res).Value, Is.EqualTo("BrandId not found"));
        }

        [Test]
        public async Task GetAllCarsAsync_ShouldReturnOkWithCars()
        {
            var cars = new List<Cars>
            {
                new Cars { Id = 1, Model="A", BrandId=1, Brand=new Brand{Id=1, Name="X"} },
                new Cars { Id = 2, Model="B", BrandId=2, Brand=new Brand{Id=2, Name="Y"} }
            };
            var viewDtos = new List<CarViewDTO>
            {
                new CarViewDTO{ Id=1, Model="A", BrandId=1, BrandName="X"},
                new CarViewDTO{ Id=2, Model="B", BrandId=2, BrandName="Y"}
            };
            _mockCarRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(cars);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(cars[0])).Returns(viewDtos[0]);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(cars[1])).Returns(viewDtos[1]);

            var res = await _controller.GetAllCarsAsync();
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            var valueList = okRes.Value as List<CarViewDTO>;
            Assert.That(valueList, Is.Not.Null);
            Assert.That(valueList.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnOk_WhenFound()
        {
            var car = new Cars { Id = 8, Model = "Sonet", BrandId = 100, Brand = new Brand { Id = 100, Name = "Kia" } };
            var viewDto = new CarViewDTO { Id = 8, Model = "Sonet", BrandId = 100, BrandName = "Kia" };
            _mockCarRepo.Setup(r => r.GetByIdAsync(8)).ReturnsAsync(car);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(car)).Returns(viewDto);

            var res = await _controller.GetByIdAsync(8);
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.That(okRes.StatusCode, Is.EqualTo(200));
            Assert.That(okRes.Value, Is.EqualTo(viewDto));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNotFound_WhenNull()
        {
            _mockCarRepo.Setup(r => r.GetByIdAsync(8888)).ReturnsAsync((Cars)null);
            var res = await _controller.GetByIdAsync(8888);
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public async Task UpdateCarAsync_ShouldReturnOk_WhenFound()
        {
            var car = new Cars { Id = 3, Model = "Swift", BrandId = 5, Brand = new Brand { Id = 5, Name = "Maruti" } };
            var updateDto = new CarCreateDTO { Model = "Swift DZire", BrandId = 5 };
            var mappedView = new CarViewDTO { Id = 3, Model = "Swift DZire", BrandId = 5, BrandName = "Maruti" };

            _mockCarRepo.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(car);
            _mockMapper.Setup(m => m.Map(updateDto, car));
            _mockCarRepo.Setup(r => r.UpdateAsync(car)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(car)).Returns(mappedView);

            var res = await _controller.UpdateCarAsync(3, updateDto);
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
            Assert.AreEqual(mappedView, okRes.Value);
        }

        [Test]
        public async Task UpdateCarAsync_ShouldReturnNotFound_WhenMissing()
        {
            _mockCarRepo.Setup(r => r.GetByIdAsync(42)).ReturnsAsync((Cars)null);
            var res = await _controller.UpdateCarAsync(42, new CarCreateDTO { Model = "NonExistent" });
            Assert.IsInstanceOf<NotFoundResult>(res);
        }

        [Test]
        public async Task DeleteCarAsync_ShouldReturnOk_WhenDeleted()
        {
            _mockCarRepo.Setup(r => r.DeleteAsync(44)).ReturnsAsync(true);
            var res = await _controller.DeleteCarAsync(44);
            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            StringAssert.Contains("Deleted car with ID 44", okRes.Value.ToString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [Test]
        public async Task DeleteCarAsync_ShouldReturnNotFound_WhenNotPresent()
        {
            _mockCarRepo.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);
            var res = await _controller.DeleteCarAsync(999);
            Assert.IsInstanceOf<NotFoundObjectResult>(res);
        }

        [Test]
        public async Task SearchCars_ShouldReturnOkWithResults()
        {
            var foundList = new List<Cars>
    {
        new Cars { Id = 1, Model = "Civic", BrandId = 2, Brand = new Brand { Id = 2, Name = "Honda" } },
        new Cars { Id = 2, Model = "Corolla", BrandId = 3, Brand = new Brand { Id = 3, Name = "Toyota" } }
    };

            var dtoList = new List<CarViewDTO>
    {
        new CarViewDTO { Id = 1, Model = "Civic", BrandId = 2, BrandName = "Honda" },
        new CarViewDTO { Id = 2, Model = "Corolla", BrandId = 3, BrandName = "Toyota" }
    };

            _mockCarRepo.Setup(r => r.SearchCarsAsync(
                "model", "brand", 2020, 15000, 50000, "Red", false))
                .ReturnsAsync(foundList);

            _mockMapper.Setup(m => m.Map<CarViewDTO>(foundList[0])).Returns(dtoList[0]);
            _mockMapper.Setup(m => m.Map<CarViewDTO>(foundList[1])).Returns(dtoList[1]);

            var res = await _controller.SearchCars("model", "brand", 2020, 15000, 50000, "Red", false);

            var okRes = res as OkObjectResult;
            Assert.IsNotNull(okRes, "Expected OkObjectResult");

            var valueList = okRes.Value as List<CarViewDTO>;
            Assert.IsNotNull(valueList, "Expected a list of CarViewDTO");
            Assert.That(valueList.Count, Is.EqualTo(2));
            Assert.That(valueList[0].BrandName, Is.EqualTo("Honda"));
            Assert.That(valueList[1].BrandName, Is.EqualTo("Toyota"));
        }


    }
}
