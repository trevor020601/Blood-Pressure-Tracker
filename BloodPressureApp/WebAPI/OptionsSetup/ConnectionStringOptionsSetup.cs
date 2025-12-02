using Microsoft.Extensions.Options;
using SharedLibrary.DataAccess;

namespace WebAPI.OptionsSetup;

public class ConnectionStringOptionsSetup(IConfiguration configuration) : IConfigureOptions<ConnectionStringOptions>
{
    private const string SectionName = "ConnectionString";

    public void Configure(ConnectionStringOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}
