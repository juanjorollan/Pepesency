using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sirve para activar un Timeline

public class Timeline : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject extra;

    //Comprueba si el que pisa el activador del timeline es un Jugador, si es así, activa el timeline
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && !other.CompareTag("Player Projectile"))
        {
            extra.SetActive(false);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            cutscene.SetActive(true);
            StartCoroutine(FinishCut());
        }
        
    }

    //Termina el timeline
    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(50);
        cutscene.SetActive(false);
    }

}



