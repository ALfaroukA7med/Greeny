using System.ComponentModel.DataAnnotations;

namespace Greeny.BLL.ModelVM.AuthVM
{
    public class RoleVM
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
