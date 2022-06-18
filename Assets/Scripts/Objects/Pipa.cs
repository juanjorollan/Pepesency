using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sirve para definir el objeto Pipa

public class Pipa : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidBody;
    public float lifetime;
    private float lifetimesSeconds;



    // Start is called before the first frame update
    void Start()
    {
        lifetimesSeconds = lifetime;
    }

    //Cuando el tiempo de vida se cumpla, se destruye el proyectil
    void Update()
    {
        lifetimesSeconds -= Time.deltaTime;
        if (lifetimesSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Se determina la velocidad y la rotación del proyectil
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidBody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    //Si colisiona con un enemigo se destruye el proyectil
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("enemykey"))
        {
            myRigidBody.velocity = Vector2.zero;
            Destroy(this.gameObject);
        }
        
    }
}
