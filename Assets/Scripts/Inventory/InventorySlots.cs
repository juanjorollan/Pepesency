using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Este script sirve para definir los Slots del menú de inventario
public class InventorySlots : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;


    public InventoryItem thisItem;
    public InventoryManagement thisManager;

    //Le asigno un objeto y un manager a un slot
    public void Setup(InventoryItem newItem, InventoryManagement newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
        }
    }

    //Indico que cuando haga click en el slot se muestre la descripción de ese objeto
    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.SetupDescription(thisItem.itemDescription);
        }
    }
}
