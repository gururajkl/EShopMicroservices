using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// LoggingBehavior is a MediatR pipeline behavior that logs information about the lifecycle and performance of request handling.
/// It logs the start and end of each request, including request details and the time taken to handle the request.
/// If the handling duration exceeds a specified threshold (3 seconds), a performance warning is logged.
/// </summary>
/// <typeparam name="TRequest">The type of the request being processed, which must implement <see cref="IRequest{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response expected from the request handler.</typeparam>
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse> where TResponse : notnull
{
    /// <summary>
    /// Intercepts the request, logs start and end of handling, and measures the time taken.
    /// If processing time exceeds the threshold, a performance warning is logged.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">The delegate representing the next action in the pipeline.</param>
    /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
    /// <returns>The response from the next action in the pipeline.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3) // if the request takes more than 3 seconds, log a warning
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.", typeof(TRequest).Name, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}
