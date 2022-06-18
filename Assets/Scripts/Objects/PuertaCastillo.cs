using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script sirve para abrir la puerta del castillo
public class PuertaCastillo : Interactable
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

    public InventoryItem pieza1;
    public InventoryItem pieza2;
    public InventoryItem pieza3;
    public InventoryItem pieza4;


    private void Update()
    {
        /*Al pulsar la tecla C, si el jugador está en rango de la puerta y tengo 4 objetos determinados en el inventario,además de 10 monedas, quitará los 4 objetos y las 10 monedas de mi inventario y abrirá la puerta
         Si no tengo los 4 objetos y las 10 monedas, aparecerá un dialogo indicando que los necesito*/
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if (playerInventoryUI.myInventory.Contains(pieza1)&& playerInventoryUI.myInventory.Contains(pieza2) && playerInventoryUI.myInventory.Contains(pieza3) && playerInventoryUI.myInventory.Contains(pieza4) && playerInventory.coins>=10)
                {
                    playerInventoryUI.myInventory.Remove(pieza1);
                    playerInventoryUI.myInventory.Remove(pieza2);
                    playerInventoryUI.myInventory.Remove(pieza3);
                    playerInventoryUI.myInventory.Remove(pieza4);
                    playerInventory.coins-=10;
                    Open();
                }
                else if (open == false)
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
        physicsCollider.enabled = false;

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
