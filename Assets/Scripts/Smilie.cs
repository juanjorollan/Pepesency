using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script es para un NPC que te dar� un ataque nuevo y convertir� en enemigo a otro NPC.
public class Smilie : Interactable
{

    public GameObject dialogBox;
    public Text dialogText;
    public List<string> dialog;
    int currentLine = 0;
    public PlayerMovement pepe;
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal2 raiseItem;

    public NPCDialog eliminar;
    public NPCDialog eliminar2;
    public NPCDialog eliminar3;

    public Animator anim;

    public GameObject pepeanciano;
    public GameObject badpepeanciano;

    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*Cuando el jugador pulse la C y este en rango del NPC, eliminar� la opci�n de hablar con otros 3 NPC.
    Adem�s de desactivar un NPC entero, que se transformar� en enemigo
    Cada vez que pulse la C bajo la condiciones anteriores, el dialogo avanzar�, incluso si sales del rango y luego vuelves a hablar, el dialogo seguira por donde lo dejaste
    Al finalizar el dialogo te dar� un objeto*/
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            eliminar.enabled = false;
            eliminar2.enabled = false;
            eliminar3.enabled = false;
            pepeanciano.SetActive(false);
            badpepeanciano.SetActive(true);
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
                    GiveItem();
                }
                else
                {
                    ItemAlreadyGiven();
                }
            }


        }


    }

    //M�todo que da un objeto al jugador (Funciona igual que los cofres)
    public void GiveItem()
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
    public void ItemAlreadyGiven()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
        playerInRange = false;
    }

    //Al entrar del area del NPC se activa la notificaci�n
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            anim.SetBool("inRange", true);
            contextOn.Raise();
            playerInRange = true;
        }
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
