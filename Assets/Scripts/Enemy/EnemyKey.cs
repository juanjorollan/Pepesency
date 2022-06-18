using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script es para enemigos que dan un Objeto al morir, es muy parecido al enemigo estándar

public enum EnemyState2
{
    idle, walk, attack, stagger
}

public class EnemyKey : MonoBehaviour
{
    public EnemyState2 currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal2 raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;

    public GameObject sombra;

    private Vector2 homePosition;

    public GameObject timeline;

    //Asigno la cantidad de vida y la posición inicial del enemigo
    private void Awake()
    {
        health = maxHealth.initialValue;
        homePosition = transform.position;
    }

    //El enemigo se coloca en su posición inicial
    private void OnEnable()
    {
        transform.position = homePosition;
    }

    //Al recibir daño le baja la vida. Cuando muera, inicia la corrutina de recompensa
    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(LootCo());
        }  
    }

    //Corrutina que deshabilita al enemigo, y genera una recompensa.
    //En caso de querer, también inicia un Timeline
    public IEnumerator LootCo()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        sombra.SetActive(false);
        MakeLoot();
        Debug.Log("Antes");
        yield return new WaitForSeconds(2f);
        Debug.Log("Despues");
        MakeLoot();
        yield return new WaitForSeconds(1f);
        if (timeline != null)
        {
            timeline.SetActive(true);
        }
        else
        {
        }
        this.gameObject.SetActive(false);
    }

    //Sirve para generar una recompensa que dejará el enemigo al morir
    private void MakeLoot()
    {
            if (!isOpen)
            {
                // Open the chest
                GiveItem();
            }
            else
            {
                // Chest is already open
                ItemAlreadyGiven();
            }
        }

    //Método que da un objeto al jugador (Funciona igual que los cofres)
    public void GiveItem()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        isOpen = true;
        playerInventoryUI.myInventory.Add(inventoryItem);
    }
    //Método que índica que el objeto ya ha sido dado al jugador (Funciona igual que los cofres)
    public void ItemAlreadyGiven()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    //Este método inicia la corrutina del Empuje y hace que el enemigo reciba daño
    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
    }

    /*Este método sirve para indicar que el enemigo ha sido golpeado
     Al ser golpeado y frenará al enemigo lo que dure el knockTime*/
    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState2.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }

}
