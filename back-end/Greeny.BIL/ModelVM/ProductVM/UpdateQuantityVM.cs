using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.ModelVM.ProductVM
{
    public class UpdateQuantityVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
