using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script sirve para asignar los 2 métodos que tiene a todos los objetos interactuables del juego
public class Interactable : MonoBehaviour
{

    public bool playerInRange;
    public Signal2 contextOn;
    public Signal2 contextOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Al entrar en rango, aparecerá la notificación con "?"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOn.Raise();
            playerInRange = true;

        }
    }

    //Al salir del rango, desaparecerá la notificación con "?"
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }

}
