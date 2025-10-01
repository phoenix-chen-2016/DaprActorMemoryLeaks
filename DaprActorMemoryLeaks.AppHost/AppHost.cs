using CommunityToolkit.Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var daprOptions = new DaprSidecarOptions
{
	AppId = "dapr-app",
	DaprGrpcPort = 50001,
	DaprHttpPort = 3500,
	MetricsPort = 9090,
	PlacementHostAddress = "localhost:50005",
	SchedulerHostAddress = "localhost:50006",
};

builder.AddProject<Projects.Server>("Server")
	.WithDaprSidecar(daprOptions);

builder.AddProject<Projects.Client>("Client");

builder.Build().Run();
