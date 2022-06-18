using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script sirve para que algunos NPCs tengan lineas de dialogo, te den un objeto al acabar el dialogo y deshabilitar el dialogo con otros NPCs.

public class NPCDialog : Interactable
{
    
   
    public PlayerMovement pepe;
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal2 raiseItem;

    [Header("Dialogo")]
    public GameObject dialogBox;
    public Text dialogText;
    public List<string> dialog;
    int currentLine = 0;

    [Header("Otros NPCs")]
    public NPCDialog eliminar;
    public NPCDialog eliminar2;
    public GameObject eliminarSmilie;
    public GameObject badsmilie;
    public GameObject cofrecoin;

    [Header("Menú Inventario")]
    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;


    //Limpio el menú del inventario
    // Start is called before the first frame update
    void Start()
    {
        playerInventoryUI.myInventory.Clear();
    }

    // Update is called once per frame
    /*Cuando el jugador pulse la C y este en rango del NPC, eliminará la opción de hablar con otros 2 NPC.
    Además de desactivar un NPC entero, que se transformará en enemigo por no hablar con el que tendrá un cofre al lado.
    Cada vez que pulse la C bajo la condiciones anteriores, el dialogo avanzará, incluso si sales del rango y luego vuelves a hablar, el dialogo seguira por donde lo dejaste
    Al finalizar el dialogo te dará un objeto*/
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            eliminar.enabled = false;
            eliminar2.enabled = false;
            eliminarSmilie.SetActive(false);
            badsmilie.SetActive(true);
            cofrecoin.SetActive(true);
            if (currentLine < dialog.Count)
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog[currentLine];
                ++currentLine;
            }
            else
            {
                currentLine = dialog.Count;
                if (!isOpen)
                {
                    // Open the chest
                    GiveLoot();
                }
                else
                {
                    // Chest is already open
                    LootAlreadyGiven();
                }
            }
            

        }
        

    }

    //Método que da un objeto al jugador (Funciona igual que los cofres)
    public void GiveLoot()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        contextOff.Raise();
        isOpen = true;
        playerInventoryUI.myInventory.Add(inventoryItem);
    }

    //Método que índica que el objeto ya ha sido dado al jugador (Funciona igual que los cofres)
    public void LootAlreadyGiven()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
        playerInRange = false;
    }

    //Al salir del area del NPC se desactiva la notificación y se cierra el dialogo
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
