using Microsoft.AspNetCore.Mvc;
using PowerplantEngieChallenge.Models;
using PowerplantEngieChallenge.Services;


namespace PowerplantEngieChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IPowerProducedService _powerProducedService;

        public ProductionPlanController(IPowerProducedService powerProducedService)
        {
            _powerProducedService = powerProducedService;

        }

        // POST /<ProductionPlanController>
        [HttpPost]
        public List<PowerProduced>? Post([FromBody] Payload payload)
        {
           

            if (!payload.IsValid())
            {
                return null;
            }

            var powerProduceds = _powerProducedService.CalculatePowerProduced(payload);

            return powerProduceds;

        }

    }
}
