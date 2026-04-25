using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.DAL.Enums
{
    public enum Status
    {
        [Display(Name = "Pending")]
        Pending = 1,

        [Display(Name = "Payment Received")]
        Confirmed = 2,

        [Display(Name = "Processing")]
        Processing = 3,

        [Display(Name = "Shipped")]
        Shipped = 4,

        [Display(Name = "Delivered")]
        Delivered = 5,

        [Display(Name = "Cancelled")]
        Cancelled = 0,

        //[Display(Name = "Refunded")]
        //Refunded = -1
    }
}
