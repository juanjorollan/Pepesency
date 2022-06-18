using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script sirve para recoger objetos del suelo y que se almacenen en el inventario (Funciona igual que un cofre)
public class ObjetoSuelto : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal2 raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    public GameObject activador;
    private SpriteRenderer sprite;
    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;



    // Start is called before the first frame update
    void Start()
    {
        sprite=this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    //Mismo funcionamiento que un cofre
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            if (!isOpen)
            {
                // Open the chest
               TakeItem();
                
            }
            else
            {
                // Chest is already open
                
                ItemAlreadyTaken();
            }
        }
    }

    //Mismo funcionamiento que un cofre (Solo que deshabilito el sprite del objeto, porque lo estoy recogiendo)
    public void TakeItem()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        contextOff.Raise();
        isOpen = true;
        sprite.enabled = false;
        playerInventoryUI.myInventory.Add(inventoryItem);
    }

    //Mismo funcionamiento que un cofre (Deshabilito el objeto del suelo para que no se quede su colisión, ya que ahora ese objeto está en el inventario)
    public void ItemAlreadyTaken()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
        activador.SetActive(true);
        playerInRange = false;
        this.gameObject.SetActive(false);

    }
}
