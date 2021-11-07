using EventSystem;

namespace Events
{
    public class MoneyAddedPayload : GameEventPayload
    {
        public int Amount;

        public MoneyAddedPayload(int amount)
        {
            Amount = amount;
        }
    }
}