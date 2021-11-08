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

    public GameObject RestaurantCameraPosition;

    public GameObject KitchenCameraPosition;

    public GameObject MainCamera;

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
        SpawnTable(transform.position, TableType.A);
        // SpawnOven(transform.position, OvenType.A);
    }

    public void OnMoneyAdded(GameEvent payload)
    {
        var moneyAdded = payload as MoneyAdded;
        PlayerData.Money += moneyAdded.Amount;
        Exchanges.MoneyUpdatedExchange.Dispatch(new MoneyUpdated(PlayerData.Money));
    }

    public void OnLocationChanged(GameEvent payload)
    {
        var locationChanged = (LocationChanged) payload;
        switch(locationChanged.Location)
        {
            case Location.Restaurant:
                MainCamera.transform.position =
                    RestaurantCameraPosition.transform.position;
                break;
            case Location.Kitchen:
                MainCamera.transform.position =
                    KitchenCameraPosition.transform.position;
                break;
            default:
                break;
        }
    }

    void SpawnCustomer()
    {
        var customerType = PlayerData.PotentialCustomers[Random.Range(0, PlayerData.PotentialCustomers.Count)];
        var prefab = PrefabFactory.Customers.Find(c => c.CustomerType == customerType);
        GameObject customerObj = Instantiate(prefab.Prefab) as GameObject;
        customerObj.transform.position = CustomerSpawnPoint.transform.position;
        var customer = customerObj.GetComponent<Customer>();
        customer.ExitPoint = CustomerSpawnPoint.transform.position;
    }

    void SpawnTable(Vector3 position, TableType tableType)
    {
        var prefab = PrefabFactory.Tables.Find(t => t.TableType == tableType);
        GameObject tableObj = Instantiate(prefab.Prefab) as GameObject;
        tableObj.transform.position = position;
    }

    void SpawnOven(Vector3 position, OvenType ovenType)
    {
        var prefab = PrefabFactory.Ovens.Find(t => t.OvenType == OvenType.A);
        GameObject tableObj = Instantiate(prefab.Prefab) as GameObject;
        tableObj.transform.position = position;
    }
}
