using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este scrip sirve para un NPC que al interactuar con el inicia un Timeline
public class Capibara : Interactable
{

    public GameObject timeline;
    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //Al pulsar la C y estar en rango, activará un Timeline y te quitará un objeto del inventario
    //En este caso te quita una moneda con la que pagas el barco
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            timeline.SetActive(true);
            playerInventoryUI.myInventory.Remove(inventoryItem);
        }
    }
}
