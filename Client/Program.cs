using Common;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
{
	builder.AddServiceDefaults();

	builder.Services.AddActors(null);
}

var app = builder.Build();
{
	app.MapDefaultEndpoints();
	app.MapGet("/", () => "Hello World!");
	app.MapGet(
		"/hello/{name}",
		async (
			[FromServices] IActorProxyFactory actorProxyFactory,
			string name,
			CancellationToken cancellationToken) =>
		{
			var actorId = new ActorId(name);

			var actor = actorProxyFactory.CreateActorProxy<IHelloActor>(
				actorId,
				ActorConsts.ActorTypeName,
				new ActorProxyOptions
				{
					UseJsonSerialization = true
				});

			return await actor.SayHelloAsync(name, cancellationToken).ConfigureAwait(false);
		});
}

app.Run();
