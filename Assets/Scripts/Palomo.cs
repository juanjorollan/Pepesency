using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script sirve para el NPC que abre la enciclopedia con la base de datos
public class Palomo : Interactable
{
    private bool isPaused;
    private bool isActive;
    public GameObject canvas;
    public GameObject activar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Al pulsar la C y estar en el area, cumpliras la misión de uno de los NPC y abrirás o cerrarás la enciclopedia, además de pausar y despausar (Según este abierta o cerrada la enciclopedia)
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            activar.SetActive(true);
            ChangeCanvas();
            ChangePause();
        }
    }

    //Cambia el estado de la Enciclopedia (De abierta a cerrada y viceversa)
    public void ChangeCanvas()
    {
        isActive = !isActive;
        if (isActive)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }

    }

    //Cambia el estado de la Pausa (De abierta a cerrada y viceversa)
    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

    }

}
