namespace Vezeeta.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials = 
        new Error("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken = 
        new Error("User.InvalidJwtToken", "Invalid JWT token", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail = 
        new Error("User.DuplicatedEmail", "Another User with Same Email is already Exists", StatusCodes.Status409Conflict);

}
