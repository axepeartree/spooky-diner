using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private GameLocation location;

    private int money;

    private UIManager uiManager;

    private Camera mainCamera;

    private Vector3 restaurantPosition;

    private Vector3 kitchenPosition;

    private List<Oven> ovens;

    private List<EnqueuedRecipeOrder> orderQueue;

    public GameObject moneyPrefab;

    void Start()
    {
        uiManager = transform.Find("UI").gameObject.GetComponent<UIManager>();
        restaurantPosition = transform.Find("Helpers").Find("Restaurant Position").transform.position;
        kitchenPosition = transform.Find("Helpers").Find("Kitchen Position").transform.position;
        mainCamera = Camera.main;

        var ovensGameObj = transform.Find("Interactives").Find("Ovens").gameObject;
        ovens = new List<Oven>();
        foreach (Transform child in ovensGameObj.transform)
            ovens.Add(child.gameObject.GetComponent<Oven>());
        orderQueue = new List<EnqueuedRecipeOrder>();

        MoveToRestaurant();
    }

    void Update()
    {
        PushEnqueuedOrders();
    }

    public void PushEnqueuedOrders()
    {
        if (orderQueue.Count == 0)
            return;

        foreach (var oven in ovens.Where(o => o.free).OrderByDescending(o => o.speed))
        {
            var order = orderQueue[0];
            orderQueue.RemoveAt(0);
            StartCoroutine(oven.CookOrder(this, order));
        }
    }

    public void EnqueueRecipeOrder(Customer customer, RecipeType recipeType)
    {
        Debug.Log($"An order was placed for {recipeType}");
        orderQueue.Add(new EnqueuedRecipeOrder
        {
            customer = customer,
            recipeType = recipeType,
            reward = 100
        });
    }

    public void AddMoney(int money)
    {
        this.money += money;
        uiManager.SetMoney(this.money.ToString());
    }

    public void MoveToRestaurant() => MoveToLocation(GameLocation.Restaurant);

    public void MoveToKitchen() => MoveToLocation(GameLocation.Kitchen);

    public void MoveToLocation(GameLocation location)
    {
        this.location = location;

        switch (this.location)
        {
            case GameLocation.Restaurant:
                mainCamera.transform.position = restaurantPosition;
                break;
            case GameLocation.Kitchen:
                mainCamera.transform.position = kitchenPosition;
                break;
        }

        uiManager.MoveToLocation(location);
    }

    public RecipeType[] GetAvailableRecipes()
    {
        return new RecipeType[] {
            RecipeType.Burger,
            RecipeType.Fries,
            RecipeType.Soup
        };
    }
}

public class EnqueuedRecipeOrder
{
    public Customer customer;
    public RecipeType recipeType;
    public int reward;
}

public enum GameLocation
{
    Restaurant,
    Kitchen,
}
