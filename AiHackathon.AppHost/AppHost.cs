var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AiHackathon_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");


builder.Build().Run();
