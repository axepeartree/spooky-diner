using Commons;
using EventSystem;

namespace Events
{
    public class LocationChanged : GameEvent
    {
        public Location Location;

        public LocationChanged(Location location)
        {
            Location = location;
        }
    }

    public class MoneyAdded : GameEvent
    {
        public int Amount;

        public MoneyAdded(int amount)
        {
            Amount = amount;
        }
    }

    public class MoneyUpdated : GameEvent
    {
        public int Amount;

        public MoneyUpdated(int amount)
        {
            Amount = amount;
        }
    }

    public class OrderFinished : GameEvent
    {
    }

    public class OrderRegistered : GameEvent
    {
        Customer Customer;
        RecipeType RecipeType;
    }
}