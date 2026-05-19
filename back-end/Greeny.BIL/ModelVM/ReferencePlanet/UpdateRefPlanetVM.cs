using Microsoft.AspNetCore.Http;
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

        [MaxLength(255)]
        public string? ShortDescription { get; set; }

        [Required, MaxLength(100)]
        public string Family { get; set; }

        public string? Image { get; set; }
        public IFormFile? UploadImage { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PlanetType { get; set; }

        public string? GrowthSeason { get; set; }

        [Range(0, 24)]
        public int SunlightReq { get; set; }

        [Required]
        public string SolidType { get; set; }

        [Range(0, 100)]
        public int WaterReq { get; set; }

        [Range(-50, 100)]
        public int TempReq { get; set; }
    }
}