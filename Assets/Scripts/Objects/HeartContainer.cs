using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script sirve para que los contenedores de corazones sean recolectables del suelo 
public class HeartContainer : PowerUp
{
    public FloatValue heartContainer;
    public FloatValue playerHealth;

    //Cuando el personaje pasa encima de un contenedor de coraz�n, en lugar de tener 3 corazones m�ximos tendr� 4 y te curar� toda la vida que te falte y lo destruye del suelo.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            heartContainer.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainer.RuntimeValue * 2;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }

}
