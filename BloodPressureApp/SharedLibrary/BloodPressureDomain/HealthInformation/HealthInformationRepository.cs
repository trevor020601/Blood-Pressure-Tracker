using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;

namespace SharedLibrary.BloodPressureDomain.HealthInformation;

[InjectDependency(ServiceLifetime.Scoped)]
public interface IHealthInformationRepository
{

}

public sealed class HealthInformationRepository : IHealthInformationRepository
{
}
