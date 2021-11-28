using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Database;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Infrastructure.Persistence.Repositories;
using Menherachan.Infrastructure.Shared.Mapping;
using Menherachan.Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Menherachan.Tests
{
    public class AdminRepositoryTests
    {
        private readonly AdminRepository _adminRepo;
        private readonly TokenRepository _tokenRepository;

        public AdminRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Menherachan").Options;

            var dbContext = new ApplicationDbContext(options);

            _adminRepo = new AdminRepository(dbContext);
            _tokenRepository = new TokenRepository(dbContext);
        }

        [Fact]
        public async void GetDataAsync_ReturnsEmptyList_WhenNoAdmins()
        {
            //Arrange


            //Act
            var actual = await _adminRepo.GetDataAsync();

            //Assert
            actual.Should().BeOfType<List<Admin>>();
            actual.Count().Should().Be(0);
        }

        [Fact]
        public async void GetDataAsync_ReturnsNotEmptyList_WhenAddAdmin()
        {
            //Arrange
            var adminId = new Random().Next(10, 100);
            var adminLogin = "MixaKonan";

            var admin = new Admin()
            {
                AdminId = adminId,
                Login = adminLogin
            };

            //Act
            await _adminRepo.AddAsync(admin);
            var actual = (List<Admin>) await _adminRepo.GetDataAsync();

            var exception = Record.Exception(() => actual[0]);

            var actualAdmin = actual[0];

            //Assert
            exception.Should().BeNull();
            actual.Should().HaveCount(1);
            actualAdmin.AdminId.Should().Be(adminId);
            actualAdmin.Login.Should().Be(adminLogin);
            actualAdmin.Email.Should().BeNull();
        }
    }

    public class AdminServiceTests
    {
        private AdminService _adminService;
        
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenRepository _tokenRepository;
        private ITokenService _tokenService;
        private IMapper _mapper;

        public AdminServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Menherachan").Options;

            var dbContext = new ApplicationDbContext(options);

            _adminRepository = new AdminRepository(dbContext);
            _tokenRepository = new TokenRepository(dbContext);
            
            _mapper = new Mapper(new MapperConfiguration(config => config.AddProfile(new GeneralProfile())));
        }

        [Fact]
        public async void AuthenticateAsync_Authenticates_OnValidCredits()
        {
            //Arrange
            
            //MOCKING INSTEAD OF USING IN-MEMORY DB
            // var adminRepoMock = new Mock<AdminRepository>();
            // adminRepoMock
            //     .Setup(repo => repo.FindAsync(admin => admin.Email == username))
            //     .Returns(Task.FromResult(expectedAdmin));
            //
            // _adminRepository = adminRepoMock.Object;
            //
            //
            // var tokenRepoMock = new Mock<TokenRepository>();
            //
            // tokenRepoMock
            //     .Setup(repo => repo.AddAsync(It.IsAny<Token>()));
            //
            // _tokenRepository = tokenRepoMock.Object;

            
            var id = new Random().Next(0, 100);
            var username = "some@email.com";
            var login = "SomeLogin";
            var password = "somePassword";
            var passwordHash = await HashingService.GetHashFromStringAsync(password);

            var expectedJwtToken = "some.jwt.token";
            
            var expectedAdmin = new Admin()
            {
                AdminId = id,
                Email = username,
                Login = login,
                PasswordHash = passwordHash,
            };

            var expectedRefreshToken = new RefreshToken()
            {
                Token = "some.refresh.token"
            };
            
            var expectedAuthenticationResponse = new AuthenticationResponse()
            {
                Email = username,
                Nickname = login,
                Bearer = expectedJwtToken
            };
            
            var tokenServiceMock = new Mock<ITokenService>();

            tokenServiceMock.Setup(service => service.GenerateJwtToken(It.IsAny<Admin>())).Returns(expectedJwtToken);
            tokenServiceMock.Setup(service => service.GenerateRefreshToken()).Returns(expectedRefreshToken);
            
            _tokenService = tokenServiceMock.Object;

            _adminService = new AdminService(_adminRepository, _tokenService, _tokenRepository, _mapper);

            //Act
            await _adminRepository.AddAsync(expectedAdmin);
            var admins = await _adminRepository.GetDataAsync();
            var actualResponse = await _adminService.AuthenticateAsync(username, password);
            var token = await _tokenRepository.GetToken(expectedRefreshToken.Token);
            
            //Assert
            actualResponse.Item1.Email.Should().Be(username);
            actualResponse.Item1.Nickname.Should().Be(login);
            actualResponse.Item1.Bearer.Should().Be(expectedJwtToken);

            actualResponse.Item2.Token.Should().Be(expectedRefreshToken.Token);

            token.Should().NotBe(null);
            token.Admin.Should().Be(expectedAdmin);
        }

        [Fact]
        public async void AuthenticateAsync_ThrowsException_OnInvalidCredentials()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}