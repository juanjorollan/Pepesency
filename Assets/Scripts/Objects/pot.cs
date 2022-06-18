using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script utilizado para destruir el objeto y que pueda dar una recompensa al hacerlo
public class pot : MonoBehaviour
{

    private Animator anim;
    public LootTable thisLoot;

    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método utilizado para activar la animación de romper e iniciar la corrutina de destruir el objeto
    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breackCo());
    }

    //Genera una recompensa al romper el objeto y destruye el propio objeto
    IEnumerator breackCo()
    {
        yield return new WaitForSeconds(.3f);
        MakeLoot();
        this.gameObject.SetActive(false);
    }

    //Indica que recompensa dará el objeto al romper y le da la posición para que aparezca donde el objeto que has roto
    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }


}
