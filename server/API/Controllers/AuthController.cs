using System.Security.Authentication;
using System.Text.Json;
using DataAccess.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using Service.Auth.Dto;
using Service.Security;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<LoginResponse> Login(
        [FromServices] UserManager<User> userManager,
        [FromServices] IValidator<LoginRequest> validator,
        [FromServices] ITokenClaimsService tokenClaimsService,
        [FromBody] LoginRequest data
    )
    {
        await validator.ValidateAndThrowAsync(data);
        var user = await userManager.FindByEmailAsync(data.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, data.Password))
            throw new AuthenticationException("Invalid login");

        var token = await tokenClaimsService.GetTokenAsync(data.Email);

        return new LoginResponse(token);
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<RegisterResponse> Register(
        IOptions<AppOptions> options,
        [FromServices] UserManager<User> userManager,
        [FromServices] IValidator<RegisterRequest> validator,
        [FromBody] RegisterRequest data
    )
    {
        await validator.ValidateAndThrowAsync(data);

        var user = new User { UserName = data.Email, Email = data.Email };
        var result = await userManager.CreateAsync(user, data.Password);
        if (!result.Succeeded)
            throw new Exception(
                JsonSerializer.Serialize(result.Errors.ToDictionary(x => x.Code, x => new[] { x.Description })));
       
        await userManager.AddToRoleAsync(user, Role.Reader);
        return new RegisterResponse(user.Email, user.UserName);
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IResult> Logout([FromServices] SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }

    [HttpGet]
    [Route("userinfo")]
    public async Task<AuthUserInfo> UserInfo([FromServices] UserManager<User> userManager)
    {
        var username = HttpContext.User.Identity?.Name ?? throw new AuthenticationException();
        var user = await userManager.FindByNameAsync(username) ?? throw new AuthenticationException();
        var roles = await userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains(Role.Admin);
        var canPublish = roles.Contains(Role.Editor) || isAdmin;
        return new AuthUserInfo(username, isAdmin, canPublish);
    }
}