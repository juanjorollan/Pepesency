using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//5 estados distintos para el protagonista. Andar, Atacar, Interactuar, Golpeado, Quieto
public enum PlayerState {
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal2 playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public GameObject stars;
    public Signal2 playerHit;

    [Header("Primer Orbe")]
    public GameObject pipa;
    public Item orbe;

    [Header("Segundo Orbe")]
    public GameObject puño;
    public Item orbe2;

    [Header("Tercer Orbe")]
    public GameObject pelotas;
    public Item orbe3;

    [Header("Cuarto Orbe")]
    public GameObject pinchos;
    public Item orbe4;

    
    public Item hacha;

    [Header("Frame Golpeado")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;



    // Start is called before the first frame update
    //Asigno valores a algunas de las variables
    void Start()
    {
        playerInventory.coins = 0;
        playerInventory.items.Clear();
        playerInventory.numberOfKeys = 0;
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    /*En este método indico que el protagonista cambiará el tipo de rigidbody cuando interactua para no ser empujado por nada.
    Despues consigo la dirección a la que apunto. Luego para atacar, cuando pulse el boton asignado y no el protagonista no se encuentre en ninguno de los estados nombrados,
    compruebo si tengo el hacha en el inventario, en tal caso, iniciaré la corrutina de ataque.
    Si pulso el boton asignado al ataque secundario, comprobaré lo mismo que en la anterior ocasion, solo que ahora en vez de un hacha comprobaré entre 4 objetos posibles e iniciaré la corrutina correspondiente.
    Por último compruebo si mi estado es Andando o quieto, para actualizar la animacion y moverme con un método*/
    void Update()
    {
        if(currentState == PlayerState.interact)
        {
            myRigidbody.isKinematic = true;
        }
        else
        {
            myRigidbody.isKinematic = false;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState!=PlayerState.interact)
        {
            if (playerInventory.CheckForItem(hacha))
            {
                StartCoroutine(AttackCo());
            }
            
        }
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.interact)
        {
            if (playerInventory.CheckForItem(orbe))
            {
                StartCoroutine(SecondAttackCo());
            }else if (playerInventory.CheckForItem(orbe2))
            {
                StartCoroutine(ThirdAttackCo());
            }
            else if (playerInventory.CheckForItem(orbe4))
            {
                StartCoroutine(FourthAttackCo());
            }
            else if (playerInventory.CheckForItem(orbe3))
            {
                StartCoroutine(FifthAttackCo());
            }

        }
        else if(currentState == PlayerState.walk ||currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }

    }

    /*Corrutina utilizada para golpear con el hacha.
    Activo la animacion de ataque, cambio mi estado a Atacando
    Por último compruebo que el estado no es Interactuando, para quitar el estado de Atacando y poner el de Andando*/
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        
        animator.SetBool("attacking", false);

        yield return new WaitForSeconds(.33f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        

    }
    /*Corrutina utilizada para disparar pipas.
    Cambio mi estado a Atacando, y creo la Pipa con el método correspondiente
    Por último compruebo que el estado no es Interactuando, para quitar el estado de Atacando y poner el de Andando*/
    private IEnumerator SecondAttackCo()
    {
        
        currentState = PlayerState.attack;
        yield return null;
        CrearPipa();
        yield return new WaitForSeconds(.33f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }


    }
    /*Corrutina utilizada para disparar puños.
    Cambio mi estado a Atacando, y creo el Puño con el método correspondiente
    Por último compruebo que el estado no es Interactuando, para quitar el estado de Atacando y poner el de Andando*/
    private IEnumerator ThirdAttackCo()
    {

        currentState = PlayerState.attack;
        yield return null;
        CrearPuño();
        yield return new WaitForSeconds(.33f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    /*Corrutina utilizada para disparar pincho.
    Cambio mi estado a Atacando, y creo el Pincho con el método correspondiente
    Por último compruebo que el estado no es Interactuando, para quitar el estado de Atacando y poner el de Andando*/
    private IEnumerator FourthAttackCo()
    {

        currentState = PlayerState.attack;
        yield return null;
        CrearPincho();
        yield return new WaitForSeconds(.33f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    /*Corrutina utilizada para disparar pelotas.
    Cambio mi estado a Atacando, y creo la Pelota con el método correspondiente
    Por último compruebo que el estado no es Interactuando, para quitar el estado de Atacando y poner el de Andando*/
    private IEnumerator FifthAttackCo()
    {

        currentState = PlayerState.attack;
        yield return null;
        CrearPelota();
        yield return new WaitForSeconds(.33f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }


    //Con este método Creo un objeto Pipa 

    private void CrearPipa()
    {
        //temp = temporal
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Pipa pipa2 = Instantiate(pipa, transform.position, Quaternion.identity).GetComponent<Pipa>();
        pipa2.Setup(temp, DireccionPipa());
    }
    //Con este método direcciono la Pipa
    Vector3 DireccionPipa()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"),animator.GetFloat("moveX"))*Mathf.Rad2Deg;
        return new Vector3(0,0,temp);
    }

    //Con este método Creo un objeto Puño
    private void CrearPuño()
    {
        //temp = temporal
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        PuñoHumo puñohumo = Instantiate(puño, transform.position, Quaternion.identity).GetComponent<PuñoHumo>();
        puñohumo.Setup(temp, DireccionPuño());
    }
    //Con este método direcciono el Puño
    Vector3 DireccionPuño()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    //Con este método Creo un objeto Pincho
    private void CrearPincho()
    {
        //temp = temporal
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Pincho pincho = Instantiate(pinchos, transform.position, Quaternion.identity).GetComponent<Pincho>();
        pincho.Setup(temp, DireccionPincho());
    }
    //Con este método direcciono el Pincho
    Vector3 DireccionPincho()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    //Con este método Creo un objeto Pelota
    private void CrearPelota()
    {
        //temp = temporal
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        TenisBonuce pelota = Instantiate(pelotas, transform.position, Quaternion.identity).GetComponent<TenisBonuce>();
        pelota.Setup(temp, DireccionPelota());
    }
    //Con este método direcciono la Pelota
    Vector3 DireccionPelota()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    /*Este método sirve para animar la obtención de un objeto
     Compruebo si lo tengo "en las manos"(Acabar de cogerlo). Activo la animación de recibir un objeto y hago que aparezca el sprite encima del protagonista, además de poner en estado Interactuando
     Si mi estado ya es Interactuando, entonces para la animación y pongo al protagonita en estado Quieto, además de quitarlo "de mis manos"*/
    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                stars.SetActive(true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                stars.SetActive(false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    /*Con este método cambio la dirección a la que mira el personaje y le muevo.
    Si ambos ejes son distintos de 0, Moverá al personaje con el método correspondiente, y cambiará la animación(dirección)*/
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    //Este método sirve para mover al personaje a una velocidad determinada
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime
            );
    }
    /*Este método sirve para comprobar y modificar los puntos de vida del Personaje.
     Si la vida es mayor que 0, se iniará la corrutina de ser Golpeado.
    Pero cuando la vida llegue a 0, saldrá la pantalla de Game Over*/
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0) 
        {
            
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
       

    }
    /*Este método sirve para indicar que el personaje ha sido golpeado
     Al ser golpeado, iniciará la corrutina de Parpadeo y cambiará la velocidad*/
    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    //Este método sirve para que el personaje parpadee en rojo para indicar que ha sido golpeado
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp ++;
        }
        triggerCollider.enabled = true;
    }

}
