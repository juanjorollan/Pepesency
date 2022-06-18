using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script es para los enemigos que solo disparan proyectiles

public class TurretEnemy : Mosk
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    //Indico cada cuanto tiempo va a disparar un proyectil
    private void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds<=0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    //Compruebo la distancia y posición del jugador y genero un proyectil que va a esa posición a una velocidad determinada
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                if (canFire)
                {

                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    changeAnim(tempVector);
                    current.GetComponent<Projectile>().Launch(tempVector.normalized);
                    canFire = false;
                    ChangeState(EnemyState.walk);
                    anim.SetBool("isShoot", true);
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("isShoot", false);
        }
    }

    

}
