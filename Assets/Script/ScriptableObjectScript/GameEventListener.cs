using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomEvent : UnityEvent<Component, object> { }
public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    public CustomEvent actions;
    private void OnEnable()
    {
        gameEvent.RegistListener(this);
    }
    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnNotified(Component sender, object data)
    {
        actions.Invoke(sender,data);
    }
}
