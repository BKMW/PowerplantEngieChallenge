namespace PowerplantEngieChallenge.Models
{
    public class Powerplant
    {
        public string name { get; set; }
        public string type { get; set; }
        public double efficiency { get; set; }
        public float pmin { get; set; }
        public float pmax { get; set; }

        public bool IsValid()
        {
            // Validate the power plant data 
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) || efficiency <= 0 || pmin < 0 || pmax <= 0)
            {
                Console.WriteLine($"Invalid data for power plant: {name}");
                return false;
            }

            return true;
        }
    }

}