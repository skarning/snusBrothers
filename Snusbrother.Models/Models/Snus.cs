using Snusbrother.Models.Models.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snusbrother.Models.Models
{
    public class Snus
    {
        public int SnusId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Enums.Strength SnusStrength { get; set; }

        public Profile Profile { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
