using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script sirve para abrir puertas

//3 tipos de puerta, solo utilizo 1 (key)
public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public Animator doorAnim;
    public PolygonCollider2D physicsCollider;

    public GameObject dialogBox;
    public Text dialogText;
    public string dialogo;

    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;

    /*Al pulsar la tecla C, si el jugador está en rango de la puerta y tengo una llave en el inventario, quitará la llave de mi inventario y abrirá la puerta
     Si no tengo la llave, aparecerá un dialogo indicando que la necesito*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) )
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if (playerInventory.numberOfKeys > 0)
                {
                    playerInventoryUI.myInventory.Remove(inventoryItem);
                    playerInventory.numberOfKeys--;
                    Open();
                }else if (open==false)
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialogo;
                }
            }
        }
    }

    //Activo la animación de abrir la puerta y quito su collider para poder pasar
    public void Open()
    {
        doorAnim.SetBool("isOpen", true);
        open = true;
        physicsCollider.enabled=false;
        
    }

    public void Close()
    {

    }

    //Si salgo del area de la puerta, desaparece la notificación y el diálogo se cierra
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
