using PowerplantEngieChallenge.Models;

namespace PowerplantEngieChallenge.Services
{
    public class PowerProducedService : IPowerProducedService
    {

        // Define a dictionary to map power plant types

        private readonly Dictionary<string, string> powerPlantTypeMap = new Dictionary<string, string>
            {
                { "windturbine", "A" },
                { "gasfired", "B" },
                { "turbojet", "C" }
            };

        public List<Powerplant> SortPowerplantsByPriority(Payload payload)
        {
            // Map the power plant types using LINQ and the dictionary

            foreach (var item in payload.powerplants)
            {
                item.type = powerPlantTypeMap.TryGetValue(item.type, out var mappedType) ? mappedType : "";
            }

            // Sort the power plants by priority

            return payload.powerplants
                .OrderBy(x => x.type)
                .ThenByDescending(x => x.efficiency)
                .ThenByDescending(x => x.pmax)
                .ToList();
        }

        public List<PowerProduced> CalculatePowerProduced(Payload payload)
        {
            var powerProduceds = new List<PowerProduced>();

            try
            {
                // Sort the power plants by priority
                List<Powerplant> powerplantsOrderedByPriority = SortPowerplantsByPriority(payload);

                // Initialize remaining Load
                float remainingLoad = payload.load;

                // Loop through the power plants in order of priority
                for (int i = 0; i < powerplantsOrderedByPriority.Count; i++)
                {
                    var item = powerplantsOrderedByPriority[i];
                    float calculatePowerProduced = 0;

                    // Check if the load is greater than or equal to the pmin

                    if (remainingLoad >= item.pmin)
                    {
                        // Calculate power produced based on the type of power plant

                        if (item.type == "A")
                        {
                            var Powerenabled = item.pmax * payload.fuels.wind / 100;
                            calculatePowerProduced = Math.Min(Powerenabled, remainingLoad);
                        }
                        else if (item.type == "B" || item.type == "C")
                        {
                            calculatePowerProduced = Math.Min(item.pmax, remainingLoad);
                        }

                    }

                    // If remaining Load is less than pmin, handle the case

                    else if (remainingLoad > 0)
                    {

                        powerProduceds[i - 1].p -= (float)Math.Round(item.pmin - remainingLoad, 1);
                        calculatePowerProduced = item.pmin;
                    }

                    // Update the remaining load after power production

                    remainingLoad -= calculatePowerProduced;



                    powerProduceds.Add(new PowerProduced
                    {
                        name = item.name,
                        p = (float)Math.Round(calculatePowerProduced, 1)
                    });


                }
                // Check if the total power produced matches the load

                var sum = powerProduceds.Sum(x => x.p);
                if (sum != payload.load)
                    Console.WriteLine("Invalid data. load should be equal power produced.");

            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, return an error response)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return powerProduceds;
        }

    }
}
