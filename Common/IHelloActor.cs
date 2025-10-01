using Dapr.Actors;

namespace Common;
public interface IHelloActor : IActor
{
	Task<string> SayHelloAsync(string name, CancellationToken cancellationToken = default);
}
