using Snusbrother.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snusbrother.Models.Models
{
    public class Profile
    {
        [Required]
        public int ProfileId { get; set; }

        public string Name { get; set; }

        public System.DateTimeOffset Birthday { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}
