using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para los proyectiles enemigos

public class Projectile : MonoBehaviour
{

    public float moveSpeed;
    public Vector2 directionToMove;
    public float lifetime;
    private float lifetimesSeconds;
    public Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        lifetimesSeconds = lifetime;
    }

    // Update is called once per frame
    //Cuando el tiempo de vida se cumpla, se destruye el proyectil
    void Update()
    {
        lifetimesSeconds -= Time.deltaTime;
        if (lifetimesSeconds <=0)
        {
            Destroy(this.gameObject);
        }
    }

    //Asigna una velocidad al proyectil
    public void Launch(Vector2 initialVel)
    {
        myRigidBody.velocity = initialVel * moveSpeed;
    }

    //El proyectil se destruye al impactar con el jugador
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
