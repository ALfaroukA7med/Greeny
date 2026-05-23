using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Greeny.BLL.ModelVM.Comment
{
    public class CommentCreateVM
    {
        //[Required(ErrorMessage = "Please write something before posting.")]
        //[StringLength(1000, ErrorMessage = "Comments cannot exceed 1000 characters.")]
        public string? UserId { get; set; }
        public string Content { get; set; } = string.Empty;

        [Required]
        public int PostId { get; set; }

    }
}
