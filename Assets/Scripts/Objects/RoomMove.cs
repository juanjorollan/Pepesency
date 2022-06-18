using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este scrip sirve para cambiar los limites de la camara al pasar por un trigger y activar un cartel con el nombre del lugar
public class RoomMove : MonoBehaviour
{

    public Vector2 cameraChange;
    public Vector3 playerChange;
    public CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    public GameObject image;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Este método sirve para cambiar los limites de la camara y mover al personaje a una zona nueva, además usa la corrutina de mostrar el nombre del lugar
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    //Activas un cartel con animación que te muestra el nombre del lugar al que acabas de entrar
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
