using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script es para crear un tipo de objeto(inventario) desde el propio unity que almacenará sus propios datos
[CreateAssetMenu]
public class Inventory : ScriptableObject {

    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;

    
    //Comprueba si el inventario contiene un objeto concreto
    public bool CheckForItem(Item item)
    {
        if (items.Contains(item)){
            return true;
        }
        return false;
    }

    //Sirve para añadir un objeto al inventario
    //Si es una llave, suma 1 a la variable de llaves
    public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if(itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

}
