using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public static class OrderItemError
    {
        public static Error NotFound
            = new Error("OrderItem.NotFound", "Order item not found", ErrorType.NotFound);
    }
}