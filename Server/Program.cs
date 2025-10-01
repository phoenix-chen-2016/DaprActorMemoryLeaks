using Common;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Server.Actors;

var builder = WebApplication.CreateBuilder(args);
{
	builder.AddServiceDefaults();

	builder.Services
		.AddActors(options =>
		{
			options.Actors.RegisterActor<HelloActor>();

			options.UseJsonSerialization = true;
			options.ActorIdleTimeout = TimeSpan.FromMinutes(5);
		});
}

var app = builder.Build();
{
	app.MapDefaultEndpoints();
	app.MapActorsHandlers();

	app.MapGet("/", () => "DaprApp is running...");
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
