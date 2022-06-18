using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script servir� de base para el resto de enemigos
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

    //Asigno la cantidad de vida y la posici�n inicial del enemigo
    private void Awake()
    {
        health = maxHealth.initialValue;
        homePosition = transform.position;
    }

    //El enemigo se coloca en su posici�n inicial
    private void OnEnable()
    {
        transform.position = homePosition;
    }

    //Al recibir da�o le baja la vida. Cuando muera, dar� una recompensa y ser� destruido
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            MakeLoot();
            this.gameObject.SetActive(false);
        }
    }

    //Sirve para generar una recompensa que dejar� el enemigo al morir
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

    //Este m�todo inicia la corrutina del Empuje y hace que el enemigo reciba da�o
    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
    }

    /*Este m�todo sirve para indicar que el enemigo ha sido golpeado
     Al ser golpeado y frenar� al enemigo lo que dure el knockTime*/
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
