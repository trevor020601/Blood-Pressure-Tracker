var builder = DistributedApplication.CreateBuilder(args);

// TODO: Figure out how to configure this...
var database = builder.AddSqlServer("database")
    .AddDatabase("BloodPressureAppDb");

builder.AddProject<Projects.WebAPI>("webapi")
    .WithReference(database)
    .WaitFor(database);

await builder.Build().RunAsync();
