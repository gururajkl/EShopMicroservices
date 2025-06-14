using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// MediatR pipeline class which handles the logging before and after the main handlers.
/// </summary>
/// <typeparam name="TRequest">Type of request.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
/// <param name="logger">ILogger object.</param>
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle Request = {Request} - Response = {Response} - Request Data = {RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next(cancellationToken);

        // After executing the main handler.
        timer.Stop();
        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                typeof(TRequest).Name, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}
