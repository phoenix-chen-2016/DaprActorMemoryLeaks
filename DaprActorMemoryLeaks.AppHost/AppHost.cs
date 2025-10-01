using CommunityToolkit.Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var daprOptions = new DaprSidecarOptions
{
	AppId = "dapr-app",
	DaprGrpcPort = 50001,
	DaprHttpPort = 3500,
	PlacementHostAddress = "localhost:50005",
	SchedulerHostAddress = "localhost:50006",
	EnableProfiling = false
};

builder.AddProject<Projects.Server>("Server")
	.WithDaprSidecar(daprOptions);

builder.AddProject<Projects.Client>("Client")
	.WithDaprSidecar(daprOptions with
	{
		AppId = "dapr-app",
		PlacementHostAddress = "localhost:50005",
		SchedulerHostAddress = "localhost:50006",
		EnableProfiling = false
	});

builder.Build().Run();
