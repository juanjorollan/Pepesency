using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sirve para que algunos NPCs tengan bordes en los que pueden caminar sin poder salir de ellos
public class BoundedNPC : Interactable
{

    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D myRigidBody;
    public Animator anim;
    public Collider2D bounds;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Update is called once per frame
    //Si no detecta un jugador se mover�
    void Update()
    {
        if (!playerInRange)
        {
            Move();
        }
        
    }

    //Mueve al personaje hasta que salga de los bordes, entonces cambiar� de direcci�n
    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.fixedDeltaTime;
        if(bounds.bounds.Contains(temp))
        {
            myRigidBody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
        
    }

    //M�todo que sirve para cambiar la direcci�n de manera aleatoria
    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.left;
                break;
            case 3:
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    //Cambio la animaci�n de direcci�n
    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }

    //Cuando el NPC choque contra un collider cambiar� la direcci�n, hasta que esta sea una distinta a la actual
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops<100)
        {
            loops++;
            ChangeDirection();
        }
    }

}
