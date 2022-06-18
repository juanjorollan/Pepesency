using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script es para los NPCs que te dan una misión, te piden algo y hasta que no lo cumplas no te vuelven a hablar ni te recompensan
public class NPCMision : Interactable
{

    public GameObject dialogBox;
    public Text dialogText;
    public List<string> dialog;
    public List<string> dialog2;
    int currentLine = 0;
    int currentLine2 = 0;
    public PlayerMovement pepe;
    public GameObject activarRecompensa;
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal2 raiseItem;
    public PlayerInventory playerInventoryUI;
    public InventoryItem inventoryItem;
    public InventoryItem inventoryItem2;

    [Header("Opcional")]
    public GameObject efecto;
    public GameObject efecto2;
    public BoundedNPC boundednpc;
    public GameObject apoyoBool;
    public GameObject comprobarDinero;
    public PowerUp powerup;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    /*Al pulsar la C si el jugador está en rango, activará el dialogo con el NPC
      El NPC te pedirá algo, y comproborá si ya lo has cumplido para poder volver a hablarte y recompensarte
      En caso de que el NPC no tenga la variable comprobarDinero vacía, te gastará una moneda y te dará un objeto
      En caso contrario, una vez hayas cumplido la misión te agradecerá y recompensará */
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            
            if (currentLine < dialog.Count)
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog[currentLine];
                ++currentLine;
            }
            else
            {
                currentLine = 3;
                dialogBox.SetActive(false);
                if (comprobarDinero!=null && playerInventory.coins >= 1 && comprobarDinero.activeInHierarchy==false)
                {
                    comprobarDinero.SetActive(true);
                    playerInventory.coins -= 1;
                    powerup.powerupSignal.Raise();
                }
                else { }
                if (activarRecompensa.activeInHierarchy)
                {
                    if (Input.GetKeyDown(KeyCode.C) && playerInRange)
                    {

                        if (currentLine2 < dialog2.Count)
                        {
                            dialogBox.SetActive(true);
                            dialogText.text = dialog2[currentLine2];
                            ++currentLine2;
                        }
                        else
                        {
                            currentLine2 = dialog2.Count;
                            if (!isOpen)
                            {
                                GiveItem();
                            }
                            else
                            {
                                ItemAlreadyGiven();
                            }
                        }


                    }
                }
                
            }


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
        contextOff.Raise();
        isOpen = true;
        playerInventoryUI.myInventory.Add(inventoryItem);
    }

    /*Método que índica que el objeto ya ha sido dado al jugador.
     Te quitará del inventario el objeto que te pide para cumplir la misión
     En algunos casos se busca un efecto alternativo al cumplir la misión*/
    public void ItemAlreadyGiven()
    {
        
        dialogBox.SetActive(false);
        raiseItem.Raise();
        playerInRange = false;
        playerInventoryUI.myInventory.Remove(inventoryItem2);
        if (efecto != null)
        {
            efecto.SetActive(true);
        }
        else { }
        if (efecto2 != null)
        {
            efecto2.SetActive(false);
        }
        else { }
        if (apoyoBool != null)
        {
            apoyoBool.SetActive(true);
            if (apoyoBool.activeInHierarchy)
            {
                boundednpc.anim.SetBool("camiseta", true);
            }

        }
        else { }


    }

    //Al salir del area del NPC se desactivará la notificación y el diálogo
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }



}