using System;
using Commons;
using UnityEngine;
using UnityEngine.Events;

public class GameDirector : MonoBehaviour
{
    public GameObject CustomerSpawnPoint;

    public PlayerData PlayerData;

    public PrefabFactory PrefabFactory;

    public GameObject RestaurantCameraPosition;

    public GameObject KitchenCameraPosition;

    public GameObject MainCamera;

    [SerializeField]
    public LocationChangedEvent LocationChangedEvent;

    [SerializeField]
    public MoneyUpdatedEvent MoneyUpdatedEvent;

    void Start()
    {
        PlayerData.Reset();

        LocationChangedEvent.Invoke(Location.Restaurant);

        if (CustomerSpawnPoint == null)
            CustomerSpawnPoint = gameObject;

        SpawnCustomer();
        SpawnTable(transform.position, TableType.A);
        // SpawnOven(transform.position, OvenType.A);
    }

    public void AddMoney(int amount)
    {
        PlayerData.Money += amount;
        MoneyUpdatedEvent.Invoke(amount.ToString());
    }

    public void GoToKitchen() => ChangeLocation(Location.Kitchen);

    public void GoToRestaurant() => ChangeLocation(Location.Restaurant);

    private void ChangeLocation(Location location)
    {
        switch(location)
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
        LocationChangedEvent.Invoke(location);
    }

    private void SpawnCustomer()
    {
        var customerType = PlayerData.PotentialCustomers[UnityEngine.Random.Range(0, PlayerData.PotentialCustomers.Count)];
        var prefab = PrefabFactory.Customers.Find(c => c.CustomerType == customerType);
        GameObject customerObj = Instantiate(prefab.Prefab) as GameObject;
        customerObj.transform.position = CustomerSpawnPoint.transform.position;
        var customer = customerObj.GetComponent<Customer>();
        customer.ExitPoint = CustomerSpawnPoint.transform.position;
    }

    private void SpawnTable(Vector3 position, TableType tableType)
    {
        var prefab = PrefabFactory.Tables.Find(t => t.TableType == tableType);
        GameObject tableObj = Instantiate(prefab.Prefab) as GameObject;
        tableObj.transform.position = position;
    }

    private void SpawnOven(Vector3 position, OvenType ovenType)
    {
        var prefab = PrefabFactory.Ovens.Find(t => t.OvenType == OvenType.A);
        GameObject tableObj = Instantiate(prefab.Prefab) as GameObject;
        tableObj.transform.position = position;
    }
}

[Serializable]
public class LocationChangedEvent : UnityEvent<Location> {}

[Serializable]
public class MoneyUpdatedEvent : UnityEvent<string> {}