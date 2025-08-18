using Vezeeta.Features.Authentication.Models;

namespace Vezeeta.Features.Authentication.Queries.Login;

public record LoginRequest(string Email, string Password) : IRequest<AuthResponse?>;