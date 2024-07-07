using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Event")]
public class GameEvent : ScriptableObject
{
    [SerializeField] List<GameEventListener> listeners = new List<GameEventListener>();

    public void Notify(Component sender, object data)
    {
        foreach (GameEventListener listener in listeners) 
        {
            listener.OnNotified(sender, data);
        }
    }
    public void RegistListener(GameEventListener listener)
    {
        if(!listeners.Contains(listener))
        { 
            listeners.Add(listener);
        }
    }

    public void RemoveListener(GameEventListener listener) 
    { 
        if(listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }


}
