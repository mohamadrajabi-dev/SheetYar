namespace SheetYar.Application.Errors;

public sealed class AuthenticationFailedException : Exception
{
    public AuthenticationFailedException()
        : base("Authentication failed.")
    {
    }
}
