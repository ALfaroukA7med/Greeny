

namespace Greeny.BLL.Admin.ModelVM.ReferencePlanet
{
    public class CreateRefPlanetVM
    {
        [Required, MaxLength(100)]
        public string CommonName { get; set; }

        [Required, MaxLength(100)]
        public string SciName { get; set; }

        [MaxLength(100)]
        public string Family { get; set; }

        public string Image { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string PlanetType { get; set; }

        [MaxLength(100)]
        public string GrowthSeason { get; set; }

        [Range(0, 10)]
        public int SunlightReq { get; set; }

        [MaxLength(100)]
        public string SolidType { get; set; }

        [Range(0, 10)]
        public int WaterReq { get; set; }

        [Range(0, 50)]
        public int TempReq { get; set; }
    }
}
