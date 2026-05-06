using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public static class OrderError
    {
        public static Error NotFound
            = new Error("Order.NotFound", "Order not found", ErrorType.NotFound);
    }
}
