using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script sirve para que las monedas sean recolectables del suelo 
public class Coin : PowerUp
{

    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cuando el personaje pasa encima de una moneda, suma 1 moneda al inventario, y destruye la moneda del suelo
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
