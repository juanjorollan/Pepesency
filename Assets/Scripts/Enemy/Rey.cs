using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Este script es para el enemigo final, atacará con un arma y tambien disparará proyectiles

public class Rey : Mosk
{

    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;
    public string mainMenu;

    void Start()
    {

    }

    // Update is called once per frame
    //Indico cada cuanto tiempo va a disparar un proyectil
    void Update()
    {
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    //Al recibir daño le baja la vida. Cuando muera, habrás completado el juego y te envía la pantalla de inicio
    public override void TakeDamage(float damage) 
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene(mainMenu);
        }
    }

    //Con este método compruebo la distancia entre el enemigo y el jugador, y muevo al anemigo a una velocidad determinada hacia el jugador.
    //Aparte de la distancia para atacar con el arma, comprueba la distancia para atacar con los proyectiles
    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
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
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }

        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("isShoot", false);
        }


    }
    //Corrutina que cambia el estado y las animaciones del enemigo a Atacando
    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("attack", false);
    }

}