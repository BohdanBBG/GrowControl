using GrowControl.Models.Enums;

namespace GrowControl.Models.Domain
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Height { get; set; }
        public int Price { get; set; }
        public string Substrate { get; set; }
        public TimeSpan GerminationTime { get; set; }
        public TimeSpan GrowthTime { get; set; }

        public DateTime Planted { get; set; }

    }
}
