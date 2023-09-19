using PowerplantEngieChallenge.Models;

namespace PowerplantEngieChallenge.Services
{
    public interface IPowerProducedService
    {
        List<Powerplant> SortPowerplantsByPriority(Payload payload);
        List<PowerProduced> CalculatePowerProduced(Payload payload);
    }
}
