using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    [CreateAssetMenu]
    public class GameEventExchange : ScriptableObject
    {
        private List<GameEventListener> listeners = 
                new List<GameEventListener>();

        public void Dispatch(GameEventPayload payload) =>
            listeners.ForEach(l => l.OnEventRaised(payload));

        public void RegisterListener(GameEventListener listener) =>
            listeners.Add(listener);

        public void UnregisterListener(GameEventListener listener) =>
            listeners.Remove(listener);
    }
}