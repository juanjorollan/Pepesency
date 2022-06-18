using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script servirá de base para el resto de enemigos
public enum EnemyState
{
    idle,walk,attack,stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public int baseAttack;
    public float moveSpeed;

    public LootTable thisLoot;

    private Vector2 homePosition;

    //Asigno la cantidad de vida y la posición inicial del enemigo
    private void Awake()
    {
        health = maxHealth.initialValue;
        homePosition = transform.position;
    }

    //El enemigo se coloca en su posición inicial
    private void OnEnable()
    {
        transform.position = homePosition;
    }

    //Al recibir daño le baja la vida. Cuando muera, dará una recompensa y será destruido
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            MakeLoot();
            this.gameObject.SetActive(false);
        }
    }

    //Sirve para generar una recompensa que dejará el enemigo al morir
    private void MakeLoot()
    {
        if (thisLoot !=null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if (current!=null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    //Este método inicia la corrutina del Empuje y hace que el enemigo reciba daño
    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
    }

    /*Este método sirve para indicar que el enemigo ha sido golpeado
     Al ser golpeado y frenará al enemigo lo que dure el knockTime*/
    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

}
