using Xunit;
using CS_Base_Project.BLL.Services.Interfaces;
using CS_Base_Project.Controllers;
using CS_Base_Project.DAL.Data.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CS_Base_Project.UnitTest.Controllers;

public class AccountControllerTests
{
    [Fact]
    public void GetAccount_ShouldThrowNotFoundException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<AccountController>>();
        var accountServiceMock = new Mock<IAccountService>();
        var controller = new AccountController(loggerMock.Object, accountServiceMock.Object);

        // Act & Assert
        var ex = Assert.Throws<NotFoundException>(() => controller.GetAccount());
        Assert.Equal("Account not found!", ex.Message);
    }
}