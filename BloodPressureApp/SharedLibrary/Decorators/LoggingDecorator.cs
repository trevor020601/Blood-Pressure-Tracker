using Microsoft.Extensions.Logging;
using SharedLibrary.Messaging;
using SharedLibrary.Result;

namespace SharedLibrary.Decorators;

internal static class LoggingDecorator
{
    internal sealed class QueryHandler<TQuery, TResponse>(IQueryHandler<TQuery, TResponse> innerHandler, 
                                                          ILogger<QueryHandler<TQuery, TResponse>> logger) 
        : IQueryHandler<TQuery, TResponse> 
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken)
        {
            var requestName = typeof(TQuery).Name;

            logger.LogInformation($"Processing request {requestName}");

            var result = await innerHandler.HandleAsync(query, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation($"Completed request {requestName}");
            } 
            else
            {
                logger.LogError($"Completed request {requestName} with error");
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(ICommandHandler<TCommand> innerHandler,
                                                      ILogger<CommandBaseHandler<TCommand>> logger)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        public async Task<Result.Result> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var requestName = typeof(TCommand).Name;

            logger.LogInformation($"Processing request {requestName}");

            var result = await innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation($"Completed request {requestName}");
            }
            else
            {
                logger.LogError($"Completed request {requestName} with error");
            }

            return result;
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> innerHandler,
                                                  ILogger<CommandHandler<TCommand, TResponse>> logger)
    : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken)
        {
            var requestName = typeof(TCommand).Name;

            logger.LogInformation($"Processing request {requestName}");

            var result = await innerHandler.HandleAsync(command, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation($"Completed request {requestName}");
            }
            else
            {
                logger.LogError($"Completed request {requestName} with error");
            }

            return result;
        }
    }
}
