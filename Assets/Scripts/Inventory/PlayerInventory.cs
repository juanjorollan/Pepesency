using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script es para crear un tipo de objeto (inventario) desde el propio unity que almacenará sus propios datos

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>(); 
}
