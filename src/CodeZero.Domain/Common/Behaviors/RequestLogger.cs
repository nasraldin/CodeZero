namespace CodeZero.Domain.Common.Behaviours;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public RequestLogger(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation($"{Assembly.GetExecutingAssembly()} Request: {requestName} {request}");

        await Task.CompletedTask;
    }
}