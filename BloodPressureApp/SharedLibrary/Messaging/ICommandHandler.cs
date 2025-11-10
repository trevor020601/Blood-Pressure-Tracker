namespace SharedLibrary.Messaging;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<Result.Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Result.Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
