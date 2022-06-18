using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script es para crear un tipo de objeto desde el propio unity que almacenará sus propios datos

//Creo una clase "Loot" para luego crear una lista de este tipo
[System.Serializable]
public class Loot
{
    public PowerUp thisLoot;
    public int lootChance;
}
//Genera un objeto de varios, cada uno con su propio porcentaje de generación
//Por ejemplo una moneda con 50% y un corazón con 50%, el enemigo que tenga esta lootTable asignada, tendrá un 50% de dar una moneda al morir y un 50% de dar un corazón
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    public PowerUp LootPowerUp()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);
        for(int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if(currentProb<=cumProb)
            {
                return loots[i].thisLoot;
            }        
        }
        return null;
    }
}
