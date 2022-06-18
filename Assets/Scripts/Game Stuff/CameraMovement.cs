using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script sirve para que la cámara persiga al personaje, limitar hasta donde puede moverse(Bordes) y hacer una animación de zoom al ser golpeado
public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    public Animator anim;
    // Start is called before the first frame update
    //Posiciono la cámara encima del personaje
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    }

    // Update is called once per frame
    //La camará se moverá junto con el personaje, hasta que la cámara llegue a unos bordes especificados
    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                target.position.y,
                transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x,
                minPosition.x,
                maxPosition.x);

            targetPosition.y = Mathf.Clamp(targetPosition.y,
                minPosition.y,
                maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, 
                targetPosition, smoothing);
        
            
        }
    }

    //Activa la animación de ser golpeado y luego la detiene
    public void BeginKick()
    {
        anim.SetBool("kick_active", true);
        StartCoroutine(KickCo());
    }

    //Detiene la animación de ser golpeado
    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kick_active", false);
    }

}
