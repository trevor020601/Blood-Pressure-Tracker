var builder = DistributedApplication.CreateBuilder(args);

// TODO: Add sql server and entity framework core configuration

builder.AddProject<Projects.WebAPI>("webapi");

await builder.Build().RunAsync();
