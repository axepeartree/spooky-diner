using Types;
using EventSystem;

namespace Events
{
    public class LocationChangedPayload : GameEventPayload
    {
        public Location Location;

        public LocationChangedPayload(Location location)
        {
            Location = location;
        }
    }
}