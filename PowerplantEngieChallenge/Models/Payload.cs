using System.ComponentModel.DataAnnotations;

namespace PowerplantEngieChallenge.Models
{

    public class Payload
    {
        [Required(ErrorMessage = "The 'load' field is required.")]
        public float load { get; set; }
        public Fuels fuels { get; set; }
        public Powerplant[] powerplants { get; set; }


        public bool IsValid()
        {
            // Validate the load value
            if (load <= 0)
            {
                Console.WriteLine("Invalid 'load' value. It should be a positive integer.");
                return false;
            }

            // Validate the powerplants list
            if (powerplants == null || !powerplants.Any() || powerplants.Any(p => !p.IsValid()))
            {
                Console.WriteLine("Invalid 'powerplants' data.");
                return false;
            }
            // Validate the fuels 
            if (fuels == null )
            {
                Console.WriteLine("Invalid 'fuels' data.");
                return false;
            }

            return true;
        }
    }

}