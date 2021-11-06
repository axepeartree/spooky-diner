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

    void Start()
    {
        uiManager = transform.Find("UI").gameObject.GetComponent<UIManager>();
        restaurantPosition = transform.Find("Helpers").Find("Restaurant Position").transform.position;
        kitchenPosition = transform.Find("Helpers").Find("Kitchen Position").transform.position;
        mainCamera = Camera.main;

        MoveToRestaurant();
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

    public void EnqueueRecipeOrder(GameObject customer, RecipeType recipeType)
    {
        Debug.Log($"An order was placed for {recipeType}");
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

public enum GameLocation
{
    Restaurant,
    Kitchen,
}
