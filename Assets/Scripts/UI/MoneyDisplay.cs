using Events;
using EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class MoneyDisplay : MonoBehaviour
    {
        Text textComponent;

        void Start() => textComponent = GetComponent<Text>();

        public void OnMoneyUpdated(GameEventPayload payload) =>
            textComponent.text = ((MoneyUpdatedPayload) payload).Amount.ToString();
    }
}