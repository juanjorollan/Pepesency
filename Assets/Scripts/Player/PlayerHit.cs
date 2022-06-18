using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sería asignado en las hitbox de los ataques, sin embargo lo uní todo en el Script de KnockBack. Lo dejo en el proyecto por si acaso hay alguna hitbox que no haya actualizado y dependa de este script.
public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable"))
        {
            other.GetComponent<pot>().Smash();
        }
    }
}
