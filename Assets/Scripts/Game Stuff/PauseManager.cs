using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Este m�todo sirve para utilizar el men� de pausa y el de inventario
public class PauseManager : MonoBehaviour
{


    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    public bool usingPauseP;
    public string mainMenu;
    // Start is called before the first frame update
    //Asigno valores para que al empezar el men� de pausa no est� activo
    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        usingPauseP = false;
    }

    //Indico que al pulsar la tecla Escape, cambiar� el estado de pausa
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
        }
    }

    //Sirve para cambiar el estado de pausa (Si est� activo, desactivarlo y viceversa)
    //Cambio el timeScale a 0 para que el juego se pause
    public void ChangePause()
    {
        isPaused = !isPaused;
            if (isPaused)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
                usingPauseP = true;
            }
            else
            {
                inventoryPanel.SetActive(false);
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        
    }

    //M�todo que se ejecuta al pulsar el bot�n de salir del men� de pausa
    //Te lleva a la pantalla de inicio
    public void QuitToMain() {
       SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    //M�todo que sirve para cambiar del men� de Pausa al del inventario
    public void SwitchPanels()
    {
        usingPauseP = !usingPauseP;
        if (usingPauseP)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(false);
            inventoryPanel.SetActive(true);
        }
    }
}
