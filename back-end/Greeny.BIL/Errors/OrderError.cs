using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public static class OrderError
    {
        public static Error NotFound
            = new Error("Order.NotFound", "Order not found", ErrorType.NotFound);
    }
}
