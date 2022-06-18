using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script es para crear un tipo de objeto desde el propio unity que servirá para mandar una señal(avisar)

[CreateAssetMenu]
public class Signal2 : ScriptableObject
{

    public List<SignalListener> listeners = new List<SignalListener>();
    //Invoca un UnityEvent cuando se use este método 
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }
    //Añado a la lista de "Recibidores/Oyente"
    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }
    //Quito de la lista de "Recibidores/Oyente"
    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }

}