using Commons;
using Events;
using EventSystem;
using UnityEngine;

public class ChangeLocationButton : MonoBehaviour
{
    public Exchanges Exchanges;

    public GameObject Button;

    public Location TargetLocation;

    public void ChangeLocation() =>
        Exchanges.LocationChangedExchange.Dispatch(
            new LocationChanged(TargetLocation));

    public void OnLocationChanged(GameEvent payload)
    {
        var locationChanged = (LocationChanged) payload;
        Button.SetActive(locationChanged.Location != TargetLocation);
    }
}