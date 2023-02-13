using System.Net;
using System.Text;
using AuthorizationService.API.ViewModels;
using Newtonsoft.Json;
using Xunit;
using static AuthorizationService.API.Tests.Constants.ApiTestsConstants;
using static AuthorizationService.API.Tests.Helpers.ClientBuilder;
using static AuthorizationService.API.Tests.Entities.TestSpeakerEntity;
using System.Net.Http.Json;
using AuthorizationService.API.Models;

namespace AuthorizationService.API.Tests;

public class TokenControllerTests
{
    [Fact]
    public async Task Login_ValidLoginViewModel_ReturnsToken()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validSpeakerEntity in GetValidSpeakerEntities)
        {
            var validLoginViewModel = new LoginViewModel()
            {
                Email = validSpeakerEntity.Email,
                Password = validSpeakerEntity.Password
            };
            
            var jsonValidLoginViewModel = JsonConvert.SerializeObject(validLoginViewModel);
            var speakerViewModelContent = new StringContent(jsonValidLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, speakerViewModelContent);

            var token = await tokenResponse.Content.ReadFromJsonAsync<TokenModel>();

            Assert.Equal(HttpStatusCode.OK, tokenResponse.StatusCode);
            Assert.NotNull(token);
        }
    }

    [Fact]
    public async Task Login_EmptyPassword_ReturnsBadRequest()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validSpeakerEntity in GetValidSpeakerEntities)
        {
            var loginViewModel = new LoginViewModel()
            {
                Email = validSpeakerEntity.Email,
                Password = ""
            };

            var jsonLoginViewModel = JsonConvert.SerializeObject(loginViewModel);
            var speakerViewModelContent = new StringContent(jsonLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, speakerViewModelContent);

            Assert.Equal(HttpStatusCode.BadRequest, tokenResponse.StatusCode);
        }
    }

    [Fact]
    public async Task Login_EmptyEmail_ReturnsBadRequest()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validSpeakerEntity in GetValidSpeakerEntities)
        {
            var loginViewModel = new LoginViewModel()
            {
                Email = "",
                Password = validSpeakerEntity.Password
            };

            var jsonLoginViewModel = JsonConvert.SerializeObject(loginViewModel);
            var speakerViewModelContent = new StringContent(jsonLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, speakerViewModelContent);

            Assert.Equal(HttpStatusCode.BadRequest, tokenResponse.StatusCode);
        }
    }

    [Fact]
    public async Task Login_InvalidLoginViewModel_ReturnsBadRequest()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validSpeakerEntity in GetValidSpeakerEntities)
        {
            var validLoginViewModel = new LoginViewModel()
            {
                Email = validSpeakerEntity.Email + "randomText",
                Password = validSpeakerEntity.Email + "randomText"
            };

            var jsonValidLoginViewModel = JsonConvert.SerializeObject(validLoginViewModel);
            var speakerViewModelContent = new StringContent(jsonValidLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, speakerViewModelContent);

            Assert.Equal(HttpStatusCode.BadRequest, tokenResponse.StatusCode);
        }
    }

    [Fact]
    public async Task RefreshToken_ValidTokenModel_ReturnsToken()
    {
        await using var application = new AuthorizationApi();
        var client = await CreateClient(application);

        foreach (var validSpeakerEntity in GetValidSpeakerEntities)
        {
            var validLoginViewModel = new LoginViewModel()
            {
                Email = validSpeakerEntity.Email,
                Password = validSpeakerEntity.Password
            };

            var jsonValidLoginViewModel = JsonConvert.SerializeObject(validLoginViewModel);
            var speakerViewModelContent = new StringContent(jsonValidLoginViewModel, Encoding.UTF8, "application/json");
            var tokenResponse = await client.PostAsync(TokenPath, speakerViewModelContent);
            var token = await tokenResponse.Content.ReadFromJsonAsync<TokenModel>();

            var jsonTokenModel = JsonConvert.SerializeObject(token);
            var tokenContent = new StringContent(jsonTokenModel, Encoding.UTF8, "application/json");

            var refreshTokenResponse = await client.PostAsync(RefreshTokenPath, tokenContent);
            var refreshToken = await refreshTokenResponse.Content.ReadFromJsonAsync<TokenModel>();

            Assert.Equal(HttpStatusCode.OK, refreshTokenResponse.StatusCode);
            Assert.NotNull(refreshToken);
        }
    }
}