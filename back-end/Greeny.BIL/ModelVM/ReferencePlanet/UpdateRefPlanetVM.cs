using System.ComponentModel.DataAnnotations;

namespace Greeny.BLL.ModelVM.ReferencePlanet
{
    public class UpdateRefPlanetVM
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string CommonName { get; set; }

        [Required, MaxLength(100)]
        public string SciName { get; set; }

        public string Family { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string PlanetType { get; set; }
        public string GrowthSeason { get; set; }
        public int SunlightReq { get; set; }
        public string SolidType { get; set; }
        public int WaterReq { get; set; }
        public int TempReq { get; set; }
    }
}
