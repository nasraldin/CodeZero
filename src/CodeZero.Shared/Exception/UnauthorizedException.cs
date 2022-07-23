namespace CodeZero.Exception;

public class UnauthorizedException : CodeZeroException
{
    public UnauthorizedException(System.Exception inner) : base("Unauthorized", inner) { }

    public static void ThrowIfUnauthorizedServiceCall(System.Exception ex)
    {
        if (ex.InnerException != null && ex.Message.Contains("Unauthorized"))
            throw new UnauthorizedException(ex);
    }
}