using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sirve para definir el objeto Pelota
public class TenisBonuce : MonoBehaviour
{

    public float speed;
    public float lifetime;
    private float lifetimesSeconds;
    public Rigidbody2D rb;
    private Vector3 LastVelocity;



    // Start is called before the first frame update
    void Start()
    {
        lifetimesSeconds = lifetime;
    }

    //Cuando el tiempo de vida se cumpla, se destruye el proyectil
    void Update()
    {
        LastVelocity = rb.velocity;
        lifetimesSeconds -= Time.deltaTime;
        if (lifetimesSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    //Se determina la velocidad y la rotación del proyectil
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        rb.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    //Si colisiona con un enemigo se destruye el proyectil
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("enemykey"))
        {
            rb.velocity = Vector2.zero;
            Destroy(this.gameObject);
        }

    }

    //Si colisiona con un Collider(No trigger) el proyectil rebotará
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            var speed = LastVelocity.magnitude;
            var direction = Vector3.Reflect(LastVelocity.normalized, other.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
        }

    }

}

    

