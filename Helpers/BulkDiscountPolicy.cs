namespace TouristApp.Helpers
{
    public sealed class BulkDiscountPolicy : IDiscountPolicy
    {
        public decimal GetDiscountPercent(int quantity)
        {
            if (quantity > 10) return 0.20m;
            if (quantity > 3) return 0.10m;
            if (quantity > 1) return 0.05m;
            return 0m;
        }
    }
}
