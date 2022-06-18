using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script es para crear un tipo de objeto desde el propio unity que almacenará sus propios datos

[CreateAssetMenu(fileName="New Item",menuName ="Inventory/Items")]

public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
  
}
