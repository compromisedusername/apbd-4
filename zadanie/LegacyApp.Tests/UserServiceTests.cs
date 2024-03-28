using System.Security.Cryptography.X509Certificates;
using Xunit.Sdk;

namespace LegacyApp.Tests;

public class UserServiceTests
{

    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService();
    }

    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        

        // Act
        var result = _userService.AddUser(
            null, 
            "Kowalski", 
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        // Assert.Equal(false, result);
        Assert.False(result);
    }
    
    // 
    // 
    // 
    // 
    // 
    // AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit
    // AddUser_ThrowsExceptionWhenUserDoesNotExist
    // AddUser_ThrowsExceptionWhenUserNoCreditLimitExistsForUser


    [Fact]
    public void AddUser_ReturnsTrueWhenImportantClient()
    {
        var result = _userService.AddUser(
            
            "Bartek",
            "Malewski", 
            "malewski@gmail.pl",
            DateTime.Parse("2000-01-01"),
            3
        );
        //Assert
        Assert.True(result);
    }
    [Fact]
    public void AddUser_ReturnsTrueWhenNormalClient()
    {
        var result = _userService.AddUser(
            
            "Bartek",
            "Andrzejewicz", 
            "andrzejewicz@wp.pl",
            DateTime.Parse("2000-01-01"),
            6
        );
        //Assert
        Assert.True((result));
    }
    
    [Fact]
    public void AddUser_ReturnsTrueWhenVeryImportantClient()
    {
        //Act
        var result = _userService.AddUser(
            
            "Bartek",
            "Malewski", 
            "malewski@gmail.pl",
            DateTime.Parse("2000-01-01"),
            2
        );
        
        //Assert
        Assert.True(result);
    }
    [Fact]
    public void AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail()
    {
        
        // Act
        var result = _userService.AddUser(
            "Bartek",
            "Kowalski", 
            "kowalskikowalskipl",
            DateTime.Parse("2000-01-01"),
            1
        );
        
        //Assert

        Assert.False(result);
    }

    [Fact]
    public void AddUser_ThrowsArgumentExceptionWhenClientDoesNotExist()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _userService.AddUser(
            "Kowalski",
            "Niesitniejacyklient!",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        ));
    }
}

