using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script sirve para administrar los contenedores de vida
public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    //En este m�todo indico cuantos corazones aparecer�n al empezar la partida
    public void InitHearts()
    {
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    //Con este m�todo voy a actualizando la cantidad de corazones que tengo cambiando su estado (Lleno, Medio y Vac�o)
    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for(int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i <= tempHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }else if (i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
