using System.Security.Cryptography.X509Certificates;
using Xunit.Sdk;

namespace LegacyApp.Tests;

public class UserServiceTests
{

    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(new FakeClientRepository(), new FakeUserCreditServices());
    }

    [Fact]
    public void AddUser_ReturnFalseWhenUserTooYoung()
    {
        var result = _userService.AddUser(
            null, 
            "Kowalski", 
            "kowalski@kowalski.pl",
            new DateTime(DateTime.Now.Year+20),
            1
        );

        // Assert
        // Assert.Equal(false, result);
        Assert.False(result);
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
    }[Fact]
    public void AddUser_ReturnsFalseWhenSecondNameIsEmpty()
    {


        // Act
        var result = _userService.AddUser(
            "Imie", 
            null, 
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        // Assert.Equal(false, result);
        Assert.False(result);
    }
    


    [Fact]
    public void AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit()
    {
        var result = _userService.AddUser(
            
            "Bartek",
            "Kowalski", 
            "malewski@gmail.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
        //Assert
        Assert.False(result);
    }
    
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
            "Kwiatkowski", 
            "kwiatkowski@wp.pl",
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
            "NonExists",
            "Niesitniejacyklient!",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        ));
    }

    [Fact]
    public void AddUser_ThrowsArgumentExceptionWhenUserNoCreditLimitExistsForUser()
    {
        Assert.Throws<ArgumentException>(() => _userService.AddUser(
            "Kwiatkowski",
            "Niesitniejacyklient!",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            6
        ));
    }
  
}

