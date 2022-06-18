using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script sirve para que los corazones sean recolectables del suelo 
public class Heart : PowerUp
{

    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Cuando el personaje pasa encima de un corazón, si la cantidad de vida es menor de la máxima, curará 1 corazón entero del personaje y el corazón desaparece del suelo.
    //En caso de estar sin dañar el corazón desaparece del suelo sin curarte
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            if (playerHealth.initialValue > heartContainers.RuntimeValue * 2f)
            {
                playerHealth.initialValue = heartContainers.RuntimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }




    }

}
