using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEventExchange Exchange;

        public UnityEvent<GameEvent> UnityEvent;

        private void OnEnable() => Exchange.RegisterListener(this);

        private void OnDisable() => Exchange.UnregisterListener(this);

        public void OnEventRaised(GameEvent payload) => UnityEvent.Invoke(payload);
    }
}