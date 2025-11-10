namespace SharedLibrary.Messaging;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result.Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
