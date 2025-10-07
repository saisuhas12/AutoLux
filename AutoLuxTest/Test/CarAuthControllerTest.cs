using NUnit.Framework;
using Moq;
using AutoLuxBackend.Controllers;
using AutoLuxBackend.DTO.AuthDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AutoLuxTest
{
    [TestFixture]
    public class CarAuthControllerTest
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<SignInManager<IdentityUser>> _mockSignInManager;
        private Mock<IConfiguration> _mockConfig;
        private CarAuthController _controller;

        [SetUp]
        public void Setup()
        {
            var userStore = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            _mockSignInManager = new Mock<SignInManager<IdentityUser>>(
                _mockUserManager.Object,
                contextAccessor.Object,
                userPrincipalFactory.Object,
                null, null, null, null);

            _mockConfig = new Mock<IConfiguration>();
            // Setup JWT config values for login tests
            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("verySecretTestKeyForUnitTest123");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            _controller = new CarAuthController(_mockUserManager.Object, _mockSignInManager.Object, _mockConfig.Object);
        }

        [Test]
        public async Task Register_ReturnsBadRequest_WhenPasswordsDontMatch()
        {
            var dto = new UserRegisterDTO
            {
                Name = "testuser",
                //Email = "test@x.com",
                Password = "A1234!",
                ConfirmPassword = "B1234!" // Not matching
            };
            var res = await _controller.Register(dto);

            Assert.IsInstanceOf<BadRequestObjectResult>(res);
            var result = (BadRequestObjectResult)res;
            Assert.That(result.Value, Is.EqualTo("Passwords do not match."));
        }

        [Test]
        public async Task Register_ReturnsOk_WhenRegistrationSuccessful_WithoutEmail()
        {
            // Arrange
            var dto = new UserRegisterDTO
            {
                Name = "myuser",
                Password = "Password@123",
                ConfirmPassword = "Password@123"
                // Email omitted
            };

            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var res = await _controller.Register(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(res);

            var value = ((OkObjectResult)res).Value;

            // Extract "message" property from the anonymous object
            var messageProp = value.GetType().GetProperty("message")?.GetValue(value, null)?.ToString();

            Assert.That(messageProp, Is.EqualTo("User registered successfully"));
        }
        [Test]
        public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
        {
            var dto = new UserRegisterDTO
            {
                Name = "myuser",
                Password = "Password@123",
                ConfirmPassword = "Password@123",
                //Email = "mail@mail.com"
            };
            var errorList = new List<IdentityError> { new IdentityError { Description = "Email exists" } };
            _mockUserManager.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(errorList.ToArray()));

            var res = await _controller.Register(dto);

            Assert.IsInstanceOf<BadRequestObjectResult>(res);
            Assert.That(((BadRequestObjectResult)res).Value, Is.AssignableTo<IEnumerable<string>>());
        }

        [Test]
        public async Task Login_ReturnsUnauthorized_WhenUserIsNull()
        {
            var dto = new UserLoginDTO { Username = "notfound", Password = "Nope" };
            _mockUserManager.Setup(m => m.FindByNameAsync(dto.Username))
                .ReturnsAsync((IdentityUser)null);

            var res = await _controller.Login(dto);

            Assert.IsInstanceOf<UnauthorizedObjectResult>(res);
            Assert.That(((UnauthorizedObjectResult)res).Value, Is.EqualTo("Invalid user"));
        }

        [Test]
        public async Task Login_ReturnsUnauthorized_WhenPasswordWrong()
        {
            var dto = new UserLoginDTO { Username = "wronguser", Password = "wrongpass" };
            var testUser = new IdentityUser { UserName = dto.Username };
            _mockUserManager.Setup(m => m.FindByNameAsync(dto.Username)).ReturnsAsync(testUser);
            _mockSignInManager.Setup(m =>
                m.CheckPasswordSignInAsync(testUser, dto.Password, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var res = await _controller.Login(dto);

            Assert.IsInstanceOf<UnauthorizedObjectResult>(res);
            Assert.That(((UnauthorizedObjectResult)res).Value, Is.EqualTo("Incorrect password"));
        }

        [Test]
        public async Task ChangePassword_ReturnsOk_WhenPasswordIsChangedSuccessfully()
        {
            // Arrange
            var username = "testuser";
            var user = new IdentityUser { UserName = username };

            // Simulate authenticated user with ClaimsPrincipal
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };

            var dto = new ChangePasswordDto
            {
                CurrentPassword = "OldPassword123",
                NewPassword = "NewPassword456"
            };

            _mockUserManager.Setup(m => m.FindByNameAsync(username))
                .ReturnsAsync(user);

            _mockUserManager.Setup(m => m.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.ChangePassword(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(((OkObjectResult)result).Value, Is.EqualTo("Password changed successfully."));
        }

    }
}
