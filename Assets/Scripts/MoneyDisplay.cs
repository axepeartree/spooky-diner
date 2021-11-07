using Events;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyDisplay : MonoBehaviour
{
    Text textComponent;

    void Start() => textComponent = GetComponent<Text>();

    public void OnMoneyUpdated(GameEvent payload) =>
        textComponent.text = ((MoneyUpdated) payload).Amount.ToString();
}