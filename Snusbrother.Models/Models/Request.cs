using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Snusbrother.Models.Models
{
    public class Request
    {
        public int RequestId { get; set; }

        public string RequestedSnus { get; set; }

        public int Amount { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public Profile Profile { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
    }
}
