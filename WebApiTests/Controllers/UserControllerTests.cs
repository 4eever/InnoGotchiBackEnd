using AutoFixture;
using BusinessAccessLayer.DTOs;
using BusinessAccessLayer.Services;
using BusinessAccessLayer.Validators;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Api.Controllers;
using Xunit;

namespace WebApiTests.Controllers
{
    public class UserControllerTests
    {
        private readonly Fixture _fixture;

        public UserControllerTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task SingUp_ReturnsOkResult_WhenValidationPasses()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userValidatorFactoryMock = new Mock<IUserValidatorFactory>();

            var controller = new UserController(userServiceMock.Object, userValidatorFactoryMock.Object);

            var userSignUpDTO = _fixture.Create<UserSignUpDTO>();

            var validatorMock = new Mock<IValidator<UserSignUpDTO>>();

            var validationResult = new ValidationResult(new List<ValidationFailure>());
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UserSignUpDTO>(), default))
                .ReturnsAsync(validationResult);

            userValidatorFactoryMock.Setup(f => f.GetValidator<UserSignUpDTO>()).Returns(validatorMock.Object);

            var userDTO = _fixture.Create<UserDTO>();
            userServiceMock.Setup(us => us.SignUp(It.IsAny<UserSignUpDTO>()))
                .ReturnsAsync(userDTO);

            // Act
            var result = await controller.SingUp(userSignUpDTO);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(userDTO);
        }

        [Fact]
        public async Task SingUp_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userValidatorFactoryMock = new Mock<IUserValidatorFactory>();

            var controller = new UserController(userServiceMock.Object, userValidatorFactoryMock.Object);

            var userSignUpDTO = _fixture.Create<UserSignUpDTO>();

            var validatorMock = new Mock<IValidator<UserSignUpDTO>>();
            var validationErrors = new List<ValidationFailure> { new ValidationFailure("PropertyName", "Error message") };
            var validationResult = new ValidationResult(validationErrors);
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UserSignUpDTO>(), default))
                .ReturnsAsync(validationResult);

            userValidatorFactoryMock.Setup(f => f.GetValidator<UserSignUpDTO>()).Returns(validatorMock.Object);

            // Act
            var result = await controller.SingUp(userSignUpDTO);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeEquivalentTo(validationErrors);
        }

        [Fact]
        public async Task LogIn_ReturnsOkResult_WhenValidationPasses()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userValidatorFactoryMock = new Mock<IUserValidatorFactory>();

            var controller = new UserController(userServiceMock.Object, userValidatorFactoryMock.Object);

            var userLogInDTO = _fixture.Create<UserLogInDTO>();

            var validatorMock = new Mock<IValidator<UserLogInDTO>>();

            var validationResult = new ValidationResult(new List<ValidationFailure>());
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UserLogInDTO>(), default))
                .ReturnsAsync(validationResult);

            userValidatorFactoryMock.Setup(f => f.GetValidator<UserLogInDTO>()).Returns(validatorMock.Object);

            var userDTO = _fixture.Create<UserDTO>();
            userServiceMock.Setup(us => us.LogIn(It.IsAny<UserLogInDTO>()))
                .ReturnsAsync(userDTO);

            // Act
            var result = await controller.LogIn(userLogInDTO);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(userDTO);
        }

        [Fact]
        public async Task LogIn_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userValidatorFactoryMock = new Mock<IUserValidatorFactory>();

            var controller = new UserController(userServiceMock.Object, userValidatorFactoryMock.Object);

            var userLogInDTO = _fixture.Create<UserLogInDTO>();

            var validatorMock = new Mock<IValidator<UserLogInDTO>>();
            var validationErrors = new List<ValidationFailure> { new ValidationFailure("PropertyName", "Error message") };
            var validationResult = new ValidationResult(validationErrors);
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UserLogInDTO>(), default))
                .ReturnsAsync(validationResult);

            userValidatorFactoryMock.Setup(f => f.GetValidator<UserLogInDTO>()).Returns(validatorMock.Object);

            // Act
            var result = await controller.LogIn(userLogInDTO);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeEquivalentTo(validationErrors);
        }

        //
        [Fact]
        public async Task UpdateUser_ReturnsOkResult_WhenValidationPasses()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userValidatorFactoryMock = new Mock<IUserValidatorFactory>();

            var controller = new UserController(userServiceMock.Object, userValidatorFactoryMock.Object);

            var userDTO = _fixture.Create<UserDTO>();

            var validatorMock = new Mock<IValidator<UserDTO>>();

            var validationResult = new ValidationResult(new List<ValidationFailure>());
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UserDTO>(), default))
                .ReturnsAsync(validationResult);

            userValidatorFactoryMock.Setup(f => f.GetValidator<UserDTO>()).Returns(validatorMock.Object);

            var userDTO2 = _fixture.Create<UserDTO>();
            userServiceMock.Setup(us => us.UpdateUser(It.IsAny<UserDTO>()))
                .ReturnsAsync(userDTO2);

            // Act
            var result = await controller.UpdateUser(userDTO);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(userDTO2);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var userValidatorFactoryMock = new Mock<IUserValidatorFactory>();

            var controller = new UserController(userServiceMock.Object, userValidatorFactoryMock.Object);

            var userDTO = _fixture.Create<UserDTO>();

            var validatorMock = new Mock<IValidator<UserDTO>>();
            var validationErrors = new List<ValidationFailure> { new ValidationFailure("PropertyName", "Error message") };
            var validationResult = new ValidationResult(validationErrors);
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<UserDTO>(), default))
                .ReturnsAsync(validationResult);

            userValidatorFactoryMock.Setup(f => f.GetValidator<UserDTO>()).Returns(validatorMock.Object);

            // Act
            var result = await controller.UpdateUser(userDTO);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeEquivalentTo(validationErrors);
        }

    }
}
