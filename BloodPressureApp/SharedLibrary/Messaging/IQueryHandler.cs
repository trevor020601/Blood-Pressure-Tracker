namespace SharedLibrary.Messaging;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result.Result<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
