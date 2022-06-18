using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using UnityEngine.Rendering; 

//Este script sirve para modificar la iluminación (dia y noche) y crear un sistema de horas y dias.

public class DayNightScript : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI dayDisplay; 
    public Volume ppv;

    

    public float tick; 
    public float seconds;
    public int mins;
    public int hours;
    public int days = 1;


    public bool activateLights; 
    public GameObject[] lights; 
    
    // Start is called before the first frame update
    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();
        
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        CalcTime();
        DisplayTime();
    }


    //Se van sumando los segundos y convirtiendo en minutos horas y días
    public void CalcTime() 
    {
        seconds += Time.fixedDeltaTime * tick; 

        if (seconds >= 60) 
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60) 
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 24) 
        {
            hours = 0;
            days += 1;
        }
        ControlPPV();
    }

    //En función de la hora, cambiará la iluminación (21:00 empieza a anochecer y 06:00 empieza a amanecer)
    public void ControlPPV() 
    {
        //ppv.weight = 0;
        if (hours >= 21 && hours < 22) 
        {
            ppv.weight = (float)mins / 60; 
            

            if (activateLights == false) 
            {
                if (mins > 45) 
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(true); 
                    }
                    activateLights = true;
                }
            }
        }


        if (hours >= 6 && hours < 7) 
        {
            ppv.weight = 1 - (float)mins / 60;
            
            if (activateLights == true) 
            {
                if (mins > 45) 
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                       
                        lights[i].SetActive(false); 
                        
                    }
                    activateLights = false;
                }
            }
        }
    }

    //Muestra la hora y el día
    public void DisplayTime() 
    {

        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins); 
        dayDisplay.text = "Day: " + days; 
    }
}
