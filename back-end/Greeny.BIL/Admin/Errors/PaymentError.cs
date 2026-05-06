using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public static class PaymentError
    {
        public static Error NotFound
            = new Error("Payment.NotFound", "Payment not found", ErrorType.NotFound);
    }
}
