using AutoFixture;
using AutoFixture.AutoMoq;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerTests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task AddUser_WhenCalled_ShouldAddUserToDatabaseAndReturnExpectedResult()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var user = fixture.Build<User>()
                              .Without(u => u.Farm)
                              .Create();

            var dbContextMock = new Mock<IApplicationContext>();
            dbContextMock.Setup(db => db.Users.Add(It.IsAny<User>()));
            dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            var userRepository = new UserRepository(dbContextMock.Object);

            // Act
            await userRepository.AddUser(user);

            // Assert
            dbContextMock.Verify(db => db.Users.Add(It.Is<User>(u => u == user)), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_WhenCalled_ShouldDeleteUserFromDatabaseAndReturnExpectedResult()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var user = fixture.Build<User>()
                              .Without(u => u.Farm)
                              .Create();

            var dbContextMock = new Mock<IApplicationContext>();
            dbContextMock.Setup(db => db.Users.FindAsync(It.IsAny<int>())).ReturnsAsync(user);

            dbContextMock.Setup(db => db.Users.Remove(It.IsAny<User>()));
            dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            var userRepository = new UserRepository(dbContextMock.Object);

            // Act
            await userRepository.DeleteUser(user.UserId);

            // Assert
            dbContextMock.Verify(db => db.Users.FindAsync(It.IsAny<int>()), Times.Once);
            dbContextMock.Verify(db => db.Users.Remove(It.Is<User>(u => u == user)), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task GetUserById_WhenCalledWithValidId_ShouldReturnExpectedUser()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var expectedUserId = 1; // replace with a valid user id
            var expectedUser = fixture.Build<User>().With(u => u.UserId, expectedUserId).Create();

            var dbContextMock = new Mock<IApplicationContext>();
            dbContextMock.Setup(db => db.Users.FindAsync(expectedUserId)).ReturnsAsync(expectedUser);

            var userRepository = new UserRepository(dbContextMock.Object);

            // Act
            var result = await userRepository.GetUserById(expectedUserId);

            // Assert
            result.Should().NotBeNull(); // Ensure the result is not null
            result.UserId.Should().Be(expectedUserId); // Ensure the correct user is returned
            result.Should().BeEquivalentTo(expectedUser); // Ensure all properties match
        }

        [Fact]
        public async Task UpdateUser_WhenCalled_ShouldUpdateUserAndReturnExpectedResult()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var user = fixture.Build<User>()
                              .Without(u => u.Farm)
                              .Create();

            var dbContextMock = new Mock<IApplicationContext>();
            dbContextMock.Setup(db => db.Users.FindAsync(It.IsAny<int>())).ReturnsAsync(user);

            dbContextMock.Setup(db => db.Users.Update(It.IsAny<User>()));
            dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            var userRepository = new UserRepository(dbContextMock.Object);

            // Act
            await userRepository.UpdateUser(user);

            // Assert
            dbContextMock.Verify(db => db.Users.FindAsync(It.IsAny<int>()), Times.Once);
            dbContextMock.Verify(db => db.Users.Update(It.Is<User>(u => u == user)), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }
    }
}
