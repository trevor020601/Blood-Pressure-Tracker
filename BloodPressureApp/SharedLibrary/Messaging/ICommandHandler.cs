namespace SharedLibrary.Messaging;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result.Result> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Result.Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}
