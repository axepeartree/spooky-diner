using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject kitchenButton;

    private GameObject restaurantButton;

    private Text moneyText;

    void Start()
    {
        kitchenButton = transform.Find("Kitchen Button").gameObject;
        restaurantButton = transform.Find("Restaurant Button").gameObject;
        moneyText = transform.Find("Money").Find("Text").GetComponent<Text>();       
        SetMoney("0");
    }

    public void SetMoney(string money) => moneyText.text = money;

    public void MoveToLocation(GameLocation location)
    {
        kitchenButton.SetActive(false);
        restaurantButton.SetActive(false);

        switch (location)
        {
            case GameLocation.Restaurant:
                kitchenButton.SetActive(true);
                break;
            case GameLocation.Kitchen:
                restaurantButton.SetActive(true);
                break;
        }
    }

}
