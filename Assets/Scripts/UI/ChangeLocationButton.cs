using Events;
using EventSystem;
using Types;
using UnityEngine;

public class ChangeLocationButton : MonoBehaviour
{
    public GameEventExchange LocationChangedExchange;

    public GameObject Button;

    public Location TargetLocation;

    public void ChangeLocation() =>
        LocationChangedExchange.Dispatch(
            new LocationChangedPayload(TargetLocation));

    public void OnLocationChanged(GameEventPayload payload)
    {
        var locationChanged = (LocationChangedPayload) payload;
        Button.SetActive(locationChanged.Location != TargetLocation);
    }
}