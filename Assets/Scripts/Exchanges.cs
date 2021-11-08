using Events;
using EventSystem;
using UnityEngine;

[CreateAssetMenu]
public class Exchanges : ScriptableObject
{
    public GameEventExchange LocationChangedExchange;

    public GameEventExchange MoneyAddedExchange;

    public GameEventExchange MoneyUpdatedExchange;

    public GameEventExchange OrderFinishedExchange;

    public GameEventExchange OrderRegisteredExchange;
}
