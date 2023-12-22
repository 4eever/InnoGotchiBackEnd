using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using BusinessAccessLayer.DTOs;
using BusinessAccessLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayerTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task SignUp_SuccessfulRegistration_ReturnsUserDTO()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var userSignUpDTO = fixture.Create<UserSignUpDTO>();
            var userDTO = fixture.Create<UserDTO>();

            var userRepositoryMock = fixture.Freeze<Mock<IUserRepository>>();
            userRepositoryMock.Setup(repo => repo.AddUser(It.IsAny<User>()))
                             .Callback<User>(user => user.UserId = 1) // Mocking the addition of user and setting an ID
                             .Returns(Task.CompletedTask);

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                             .ReturnsAsync(fixture.Create<User>());

            var mapperMock = fixture.Freeze<Mock<IMapper>>();
            mapperMock.Setup(mapper => mapper.Map<UserSignUpDTO, User>(userSignUpDTO))
                       .Returns(fixture.Create<User>());

            mapperMock.Setup(mapper => mapper.Map<User, UserDTO>(It.IsAny<User>()))
                       .Returns(userDTO);

            var userService = fixture.Create<UserService>();

            // Act
            var result = await userService.SignUp(userSignUpDTO);

            // Assert
            result.Should().BeEquivalentTo(userDTO);
            userRepositoryMock.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task LogIn_SuccessfulLogIn_ReturnsUserDTO()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var userLogInDTO = fixture.Create<UserLogInDTO>();
            var userDTO = fixture.Create<UserDTO>();

            var userRepositoryMock = fixture.Freeze<Mock<IUserRepository>>();
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                             .ReturnsAsync(fixture.Create<User>());

            var mapperMock = fixture.Freeze<Mock<IMapper>>();
            mapperMock.Setup(mapper => mapper.Map<UserLogInDTO, User>(userLogInDTO))
                       .Returns(fixture.Create<User>());

            mapperMock.Setup(mapper => mapper.Map<User, UserDTO>(It.IsAny<User>()))
                       .Returns(userDTO);

            var userService = fixture.Create<UserService>();

            // Act
            var result = await userService.LogIn(userLogInDTO);

            // Assert
            result.Should().BeEquivalentTo(userDTO);
            userRepositoryMock.Verify(repo => repo.GetUserByEmail(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_SuccessfulUpdate_ReturnsUserDTO()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var userDTO = fixture.Create<UserDTO>();

            var userRepositoryMock = fixture.Freeze<Mock<IUserRepository>>();
            userRepositoryMock.Setup(repo => repo.UpdateUser(It.IsAny<User>()))
                             .Returns(Task.CompletedTask);

            userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                             .ReturnsAsync(fixture.Create<User>());

            var mapperMock = fixture.Freeze<Mock<IMapper>>();
            mapperMock.Setup(mapper => mapper.Map<UserDTO, User>(userDTO))
                       .Returns(fixture.Create<User>());

            mapperMock.Setup(mapper => mapper.Map<User, UserDTO>(It.IsAny<User>()))
                       .Returns(userDTO);

            var userService = fixture.Create<UserService>();

            // Act
            var result = await userService.UpdateUser(userDTO);

            // Assert
            result.Should().BeEquivalentTo(userDTO);
            userRepositoryMock.Verify(repo => repo.UpdateUser(It.IsAny<User>()), Times.Once);
        }
    }
}
