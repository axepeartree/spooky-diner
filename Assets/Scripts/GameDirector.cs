using Commons;
using Events;
using EventSystem;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject CustomerSpawnPoint;

    public PlayerData PlayerData;

    public Exchanges Exchanges;

    public PrefabFactory PrefabFactory;

    void Start()
    {
        PlayerData.Reset();
        Exchanges.LocationChangedExchange
            .Dispatch(new LocationChanged(Location.Restaurant));
        Exchanges.MoneyUpdatedExchange
            .Dispatch(new MoneyUpdated(PlayerData.Money));

        if (CustomerSpawnPoint == null)
            CustomerSpawnPoint = gameObject;

        SpawnCustomer();
        SpawnTable(transform.position);
    }

    public void OnMoneyAdded(GameEvent payload)
    {
        var moneyAdded = payload as MoneyAdded;
        Debug.Log($"Money added: {moneyAdded.Amount}.");
        PlayerData.Money += moneyAdded.Amount;
        Exchanges.MoneyUpdatedExchange.Dispatch(new MoneyUpdated(PlayerData.Money));
    }

    void SpawnCustomer()
    {
        var customerType = PlayerData.PotentialCustomers[Random.Range(0, PlayerData.PotentialCustomers.Count)];
        var prefab = PrefabFactory.Customers.Find(c => c.CustomerType == customerType);
        GameObject customerObj = Instantiate(prefab.Prefab) as GameObject;
        customerObj.transform.position = CustomerSpawnPoint.transform.position;
    }

    void SpawnTable(Vector3 position)
    {
        var prefab = PrefabFactory.Tables.Find(t => t.TableType == TableType.A);
        GameObject tableObj = Instantiate(prefab.Prefab) as GameObject;
        tableObj.transform.position = position;
    }
}
