using Newtonsoft.Json;
using ShackProfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShackProfiles.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedShackers()
        {
            var shackerData = System.IO.File.ReadAllText("data\\ShackProfiles.json");
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var shackers = JsonConvert.DeserializeObject<List<ShackProfile>>(shackerData, settings);
            

            foreach (var shacker in shackers)
            {
                shacker.DisplayShackname = shacker.Shackname;
                shacker.Shackname = shacker.Shackname.ToUpper();

                _context.ShackProfiles.Add(shacker);
            }

            _context.SaveChanges();
        }
    }
}
