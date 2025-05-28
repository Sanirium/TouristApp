using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristApp.Helpers
{
    public interface IDiscountPolicy
    {
        decimal GetDiscountPercent(int quantity);
    }

    public sealed class BulkDiscountPolicy : IDiscountPolicy
    {
        public decimal GetDiscountPercent(int quantity) =>
            quantity > 1 ? 0.05m : 0m;
    }
}
