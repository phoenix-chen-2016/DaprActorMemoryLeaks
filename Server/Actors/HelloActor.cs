using Common;
using Dapr.Actors.Runtime;

namespace Server.Actors;

[Actor(TypeName = ActorConsts.ActorTypeName)]
public class HelloActor(ActorHost actorHost)
	: Actor(actorHost)
	, IHelloActor
{
	public Task<string> SayHelloAsync(string name, CancellationToken cancellationToken = default)
		=> Task.FromResult($"Hello, {name}!");
}