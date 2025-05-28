namespace TouristApp.Helpers
{
    public interface IDiscountPolicy
    {
        decimal GetDiscountPercent(int quantity);
    }
}
