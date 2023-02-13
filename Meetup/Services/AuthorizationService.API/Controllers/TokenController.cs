using AuthorizationService.API.Models;
using AuthorizationService.API.ViewModels;
using Meetup.BLL.Interfaces;
using Meetup.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationService.API.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ISpeakerService<SpeakerModel, int> _speakerService;

    public TokenController(IConfiguration config, ISpeakerService<SpeakerModel, int> speakerService)
    {
        _configuration = config;
        _speakerService = speakerService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (string.IsNullOrEmpty(loginViewModel.Email) ||
            string.IsNullOrEmpty(loginViewModel.Password))
        {
            return BadRequest();
        }

        var speaker = await GetSpeaker(loginViewModel.Email, loginViewModel.Password);

        if (speaker is null)
        {
            return BadRequest("Invalid credentials");
        }

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, speaker.Email),
        };

        var accessToken = CreateToken(claims);
        var newRefreshToken = GenerateRefreshToken();
        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out var refreshTokenValidityInDays);

        speaker.RefreshToken = newRefreshToken;
        speaker.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await _speakerService.Update(speaker, default);

        return Ok(
            new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = newRefreshToken,
                ExpiryTime = speaker.RefreshTokenExpiryTime
            });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {
        var accessToken = tokenModel.AccessToken;
        var refreshToken = tokenModel.RefreshToken;

        var principal = GetPrincipalFromExpiredToken(accessToken);

        if (principal?.Identity?.Name is null)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var email = principal.Identity.Name;

        var speaker = await _speakerService.GetByEmail(email, default);

        if (speaker is null || speaker.RefreshToken != refreshToken || speaker.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = CreateToken(principal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out var refreshTokenValidityInDays);

        speaker.RefreshToken = newRefreshToken;
        speaker.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await _speakerService.Update(speaker, default);
        return Ok(
        new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
            ExpiryTime = speaker.RefreshTokenExpiryTime
        });
    }

    private async Task<SpeakerModel?> GetSpeaker(string email, string password)
    {
        var speakers = await _speakerService.GetAll(default);
        var speaker = speakers.FirstOrDefault(u => u.Email == email && u.Password == password);

        return speaker;
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }

    private JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out var tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
