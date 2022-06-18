using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script aplica las funciones de cartel a un objeto, leerlo.
public class Sign : Interactable
{

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //Si pulsas la C y est�s en el rango del cartel, se mostrar� el dialogo.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    //Al salir del rango del cartel, desaparece la notificaci�n y el dialogo
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

}
