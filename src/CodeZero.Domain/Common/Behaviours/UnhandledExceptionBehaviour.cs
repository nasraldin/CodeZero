namespace CodeZero.Domain.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (CodeZeroException ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, $"{Assembly.GetExecutingAssembly()} Request: Unhandled Exception for Request {requestName} {request}");

            throw;
        }
    }
}