using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SheetYar.Api.Validation;
using SheetYar.Application.Authentication;
using SheetYar.Application.Errors;

namespace SheetYar.Api.Authentication;

public static class AuthenticationEndpoint
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/v1/auth")
            .WithTags("Authentication");

        group.MapPost("/register", RegisterAsync)
            .AllowAnonymous()
            .RequireRateLimiting(AuthRateLimitPolicies.Register)
            .Validate<RegisterRequest>()
            .Produces<AuthSession>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity);
        group.MapPost("/login", LoginAsync)
            .AllowAnonymous()
            .RequireRateLimiting(AuthRateLimitPolicies.Login)
            .Validate<LoginRequest>()
            .Produces<AuthSession>()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity);
        group.MapPost("/refresh", RefreshAsync)
            .AllowAnonymous()
            .RequireRateLimiting(AuthRateLimitPolicies.Refresh)
            .Validate<RefreshRequest>()
            .Produces<AuthSession>()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem(StatusCodes.Status422UnprocessableEntity);
        group.MapPost("/logout", LogoutAsync)
            .RequireAuthorization()
            .RequireRateLimiting(AuthRateLimitPolicies.Authenticated)
            .Validate<RefreshRequest>()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
        group.MapPost("/revoke-all", RevokeAllAsync)
            .RequireAuthorization()
            .RequireRateLimiting(AuthRateLimitPolicies.Authenticated)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status401Unauthorized);
        group.MapGet("/me", MeAsync)
            .RequireAuthorization()
            .RequireRateLimiting(AuthRateLimitPolicies.Authenticated)
            .Produces<AuthenticatedUser>()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        return endpoints;
    }

    private static async Task<IResult> RegisterAsync(
        RegisterRequest request,
        HttpContext httpContext,
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var session = await authenticationService.RegisterAsync(
            request.Email,
            request.Password,
            request.DisplayName,
            GetClientIpAddress(httpContext),
            cancellationToken);
        return Results.Json(session, statusCode: StatusCodes.Status201Created);
    }

    private static async Task<IResult> LoginAsync(
        LoginRequest request,
        HttpContext httpContext,
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var session = await authenticationService.LoginAsync(
            request.Email,
            request.Password,
            GetClientIpAddress(httpContext),
            cancellationToken);
        return Results.Ok(session);
    }

    private static async Task<IResult> RefreshAsync(
        RefreshRequest request,
        HttpContext httpContext,
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var session = await authenticationService.RefreshAsync(
            request.RefreshToken,
            GetClientIpAddress(httpContext),
            cancellationToken);
        return Results.Ok(session);
    }

    private static async Task<IResult> LogoutAsync(
        RefreshRequest request,
        ClaimsPrincipal principal,
        HttpContext httpContext,
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        await authenticationService.LogoutAsync(
            GetUserId(principal),
            request.RefreshToken,
            GetClientIpAddress(httpContext),
            cancellationToken);
        return Results.NoContent();
    }

    private static async Task<IResult> RevokeAllAsync(
        ClaimsPrincipal principal,
        HttpContext httpContext,
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        await authenticationService.RevokeAllAsync(
            GetUserId(principal),
            GetClientIpAddress(httpContext),
            cancellationToken);
        return Results.NoContent();
    }

    private static async Task<IResult> MeAsync(
        ClaimsPrincipal principal,
        IAuthenticationService authenticationService,
        CancellationToken cancellationToken)
    {
        var user = await authenticationService.GetCurrentUserAsync(
            GetUserId(principal),
            cancellationToken);
        return Results.Ok(user);
    }

    private static Guid GetUserId(ClaimsPrincipal principal)
    {
        var value = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return Guid.TryParse(value, out var userId)
            ? userId
            : throw new AuthenticationFailedException();
    }

    private static string? GetClientIpAddress(HttpContext httpContext) =>
        httpContext.Connection.RemoteIpAddress?.ToString();

    public sealed record RegisterRequest(
        [property: Required, EmailAddress, MaxLength(320)] string Email,
        [property: Required, MinLength(10), MaxLength(128)] string Password,
        [property: Required, MinLength(2), MaxLength(120)] string DisplayName);

    public sealed record LoginRequest(
        [property: Required, EmailAddress, MaxLength(320)] string Email,
        [property: Required, MaxLength(128)] string Password);

    public sealed record RefreshRequest(
        [property: Required, MinLength(32), MaxLength(512)] string RefreshToken);
}
