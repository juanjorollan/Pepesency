using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Cuando una señal se "suba/envie" (Raise) este script la recibirá y utilizará un UnityEvent
public class SignalListener : MonoBehaviour
{

    public Signal2 signal;
    public UnityEvent signalEvent;

    //Invoca un UnityEvent
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    //Añado a la lista de "Recibidores/Oyente"
    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    //Quito de la lista de "Recibidores/Oyente"
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
