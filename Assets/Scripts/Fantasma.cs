using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : Interactable
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Activa la animación del fantasma y aparece una notificación
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            anim.SetBool("inRange", true);
            contextOn.Raise();
            playerInRange = true;
        }
    }

    //Desactiva la animación del fantasma y desaparece la notificación
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            anim.SetBool("inRange", false);
            contextOff.Raise();
            playerInRange = false;
        }
    }

}
