using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Script empleado en el Menú de inicio para dar funciones a los botones
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Empieza una nueva partida
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    //Cierra el juego
    public void QuitToDesktop()
    {
        Application.Quit();
    }

}
