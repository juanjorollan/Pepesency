using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script es para los enemigos que actuan solo si entras a un area especifica y no pueden salir de ese area
public class AreaEnemy : Mosk
{
    public Collider2D boundary;

    //Compruebo el area en la que puede actuar el enemigo, el resto es igual al enemigo normal
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetBool("wakeUp", false);
        }
    }
}
