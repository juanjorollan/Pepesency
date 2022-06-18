using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script funciona como el de RoomMove, sin embargo este te teletransporta a otro lugar y en lugar de cambiar los limites de la camara, cambias de camara.
public class SceneSwitch : MonoBehaviour
{

    public GameObject[] cam;
    
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    public GameObject image;
    public Vector3 playerChange;
    public GameObject fade;
    public GameObject scene;
    public GameObject pepe;

    public List<GameObject> extras;
    public List<GameObject> fadeObjects;

    public float fadetime;

    public bool fadeThis;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Al entrar al trigger, se desactivarán las cámaras que no sean la del lugar al que vas, dejando activa está última
    //Moverá al personaje a la zona nueva y se verá una transición de fundido a negro
    //También se mostrará el cartel con el nombre del lugar al que vas
    //Para que aparezca todo en negro, se desactivan momentaneamente algunos objetos que se muestran antes que la propia escena
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for(int i = 0; i < cam.Length; i++)
            {
                if (i.Equals(cam.Length-1))
                {
                    cam[i].SetActive(true);
                }
                else
                {
                    cam[i].SetActive(false);
                }
            }
            
            other.transform.position += playerChange;
            StartCoroutine(FadeCo());
            StartCoroutine(ExtrasCo());

            
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    //Activa la transición de fundido a negro, desactiva momentaneamente la escena y al personaje para que no cargen antes que la transición.
    public IEnumerator FadeCo()
    {
        if (fade != null)
        {
            fade.SetActive(true);
            scene.SetActive(false);
            pepe.SetActive(false);
           
        }
        yield return new WaitForSeconds(fadetime);
        fade.SetActive(false);
        scene.SetActive(true);
        pepe.SetActive(true);
        

    }

    //Desactiva los objetos que quieras para que no carguen antes que la transición acabe
    public IEnumerator ExtrasCo()
    {
        
        if(extras.Count>0)
        {
            for (int i = 0; i < extras.Count; i++)
            {
                if (extras[i].activeSelf)
                {
                    extras[i].SetActive(false);
                    if (!fadeObjects.Contains(extras[i]))
                    {
                        fadeObjects.Add(extras[i]);
                    }
                }
                else
                {
                    fadeObjects.Remove(extras[i]);
                }
            }
            yield return new WaitForSeconds(fadetime);
            for (int j = 0; j < fadeObjects.Count; j++)
            {
                fadeObjects[j].SetActive(true);
            }

        }
    }

    //Muestra el nombre del lugar al que vas
    private IEnumerator placeNameCo()
    {
        new WaitForEndOfFrame();
        image.SetActive(true);
        text.SetActive(true);
        anim.SetBool("Active", true);
        placeText.text = placeName;
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Active", false);
        yield return new WaitForSeconds(1f);
        image.SetActive(false);
        text.SetActive(false);
    }
}

