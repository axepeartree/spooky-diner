using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEventExchange Exchange;

        public UnityEvent<GameEventPayload> UnityEvent;

        private void OnEnable() => Exchange.RegisterListener(this);

        private void OnDisable() => Exchange.UnregisterListener(this);

        public void OnEventRaised(GameEventPayload payload) => UnityEvent.Invoke(payload);
    }
}