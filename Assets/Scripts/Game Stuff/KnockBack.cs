using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script se utiliza para empujar enemigos, al jugador y romper objetos.
public class KnockBack : MonoBehaviour
{

    public float thrust;
    public float knockTime;
    public float damage;


    /*Compruebo si el objeto que recibe el ataque del jugador se puede romper, y si es así lo rompo con el metodo del jarrón.
      Compruebo si el objeto que recibe el ataque es un Enemigo o el Jugador
      Le doy fuerza al golpe y vuelvo a comprobar quien ha recibido el golpe
      Si es un enemigo, le pongo en estado Golpeado y hago que reciba el daño correspondiente
      Si es el jugador, compruebo que no esté interactuando o Golpeado, y hago lo mismo que con el enemigo*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("enemykey"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit,knockTime, damage);
                }
                if (other.gameObject.CompareTag("enemykey") && other.isTrigger)
                {
                    hit.GetComponent<EnemyKey>().currentState = EnemyState2.stagger;
                    other.GetComponent<EnemyKey>().Knock(hit, knockTime, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger && other.GetComponent<PlayerMovement>().currentState != PlayerState.interact)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                    
                }

            }
        }
    }

}
