using EventSystem;

namespace Events
{
    public class MoneyUpdatedPayload : GameEventPayload
    {
        public int Amount;

        public MoneyUpdatedPayload(int amount)
        {
            Amount = amount;
        }
    }
}