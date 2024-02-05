using Microsoft.Extensions.Logging;
using MediatR;
using System.Text.Json;
using System.Diagnostics;
namespace Tekton.Application.UseCases.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {            var requestName = request.GetType().Name;
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("CleanArchitecture Request Handling: {name} {@request}", typeof(TRequest).Name, JsonSerializer.Serialize(request));
            var response = await next();
            _logger.LogInformation("CleanArchitecture Response Handling: {name} {@response}", typeof(TRequest).Name, JsonSerializer.Serialize(response));
            stopwatch.Stop();
            _logger.LogInformation($"[END] {typeof(TRequest).Name}; Execution time={stopwatch.ElapsedMilliseconds}ms");
            return response;
        }
    }
}
