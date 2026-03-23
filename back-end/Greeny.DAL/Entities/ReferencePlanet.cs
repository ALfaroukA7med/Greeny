using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greeny.Models
{
    public class ReferencePlanet
    {
        public int Id { get; set; }

        public string CommonName { get; set; }

        public string SciName { get; set; }

        public string Family {  get; set; }

        public string Image {  get; set; }
        public string Description { get; set; }

        public string PlanetType { get; set; }

        public string GrowthSeason { get; set; }
        
        public int SunlightReq { get; set; }

        public string SolidType { get; set; }

        public int WaterReq { get; set; }

        public int TempReq { get; set; }

    }
}
