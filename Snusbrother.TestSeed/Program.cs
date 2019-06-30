using Snusbrother.DataAccess;
using Snusbrother.Models.Models;
using Snusbrother.Models.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusbrother.TestSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            var dB = new SnusbrotherDataContext();
            var profile1 = new Profile
            {
                Name = "Sivert",
                Username = "skarndog",
                Password = "Skarning123"
            };

            var snus1 = new Snus
            {
                Name = "g3",
                Price = 50.0,
                Profile = profile1
            };

            var snus2 = new Snus
            {
                Name = "06",
                Price = 40.0,
                Profile = profile1
            };
            dB.Snus.Add(snus1);
            dB.Snus.Add(snus2);

            dB.SaveChanges();
            Console.Out.WriteLine("Finished");
        }
    }
}
