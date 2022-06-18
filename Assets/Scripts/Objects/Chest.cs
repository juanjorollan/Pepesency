using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script sirve para que los cofres tengan un contenido y el personaje pueda recibir el objeto que estos contienen
public class Chest : Interactable
{

    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal2 raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;


    public GameObject timeline;

    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*Al pulsar la tecla X, si el personaje esta en rango, comprobar� si el cofre ya ha sido abierto
      Si no est� abierto, lo abre, y si ya est� abierto, acaba la animaci�n y el dialogo y no puedes volver a abrirlo*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && playerInRange)
        {
            if (!isOpen)
            {
                // Open the chest
                OpenChest();
                
            }
            else
            {
                // Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    /*Con este m�todo abro el cofre.
    Activo el dialogo y le doy la frase. A�ado el objeto al inventario y mando la se�al de obtener un objeto, activando as� la animaci�n y dialogos correspondientes
    Mando la se�as de ContextOff para que no me notifique mas que puedo interactuar con este cofre. Activo la animaci�n de abrir el cofre y meto el objeto al men� de inventario*/
    public void OpenChest()
    {
        
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        contextOff.Raise();
        isOpen = true;
        anim.SetBool("opened", true);
        playerInventoryUI.myInventory.Add(inventoryItem);
    }

    /*Este m�todo desactiva el dialogo del cofre, asi como la animaci�n de recibir un objeto.
     Tambien lo utilizo para activar un Timeline en caso de que se necesite*/
    public void ChestAlreadyOpen()
    {
        
        dialogBox.SetActive(false);
        raiseItem.Raise();
        playerInRange = false;
        if (timeline != null)
        {
            timeline.SetActive(true);
        }
        else
        {
        }
    }

    //Si el jugador entra en el area de interacci�n del cofre, se activa la notificaci�n con una "?"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    //Si el jugador sale del area de interacci�n del cofre, se desactiva la notificaci�n con una "?"
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}
