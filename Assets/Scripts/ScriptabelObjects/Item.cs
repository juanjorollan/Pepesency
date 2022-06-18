using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script es para crear un tipo de objeto desde el propio unity que almacenará sus propios datos

[CreateAssetMenu]
public class Item : ScriptableObject
{

    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;

}