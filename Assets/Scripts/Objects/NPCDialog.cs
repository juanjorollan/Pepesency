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

    [Header("Men� Inventario")]
    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;


    //Limpio el men� del inventario
    // Start is called before the first frame update
    void Start()
    {
        playerInventoryUI.myInventory.Clear();
    }

    // Update is called once per frame
    /*Cuando el jugador pulse la C y este en rango del NPC, eliminar� la opci�n de hablar con otros 2 NPC.
    Adem�s de desactivar un NPC entero, que se transformar� en enemigo por no hablar con el que tendr� un cofre al lado.
    Cada vez que pulse la C bajo la condiciones anteriores, el dialogo avanzar�, incluso si sales del rango y luego vuelves a hablar, el dialogo seguira por donde lo dejaste
    Al finalizar el dialogo te dar� un objeto*/
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

    //M�todo que da un objeto al jugador (Funciona igual que los cofres)
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

    //M�todo que �ndica que el objeto ya ha sido dado al jugador (Funciona igual que los cofres)
    public void LootAlreadyGiven()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
        playerInRange = false;
    }

    //Al salir del area del NPC se desactiva la notificaci�n y se cierra el dialogo
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
