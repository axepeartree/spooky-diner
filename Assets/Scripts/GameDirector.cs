using Events;
using EventSystem;
using Types;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameEventExchange LocationChangedExchange;

    public GameEventExchange MoneyUpdatedExchange;

    public int Money;

    void Start()
    {
        LocationChangedExchange
            .Dispatch(new LocationChangedPayload(Location.Restaurant));
    }

    public void OnMoneyAdded(GameEventPayload payload)
    {
        var moneyAdded = payload as MoneyAddedPayload;
        Money += moneyAdded.Amount;
        MoneyUpdatedExchange.Dispatch(new MoneyUpdatedPayload(Money));
    }
}