using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Este script sirve para gestionar el menú de inventario, donde se mostrarán los obetos que tengo y una descripción de los mismos
public class InventoryManagement : MonoBehaviour
{
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public InventorySlots content;
    public InventorySlots content2;
    public InventorySlots content3;
    public InventorySlots content4;
    public InventorySlots content5;
    public InventorySlots content6;
    public InventorySlots content7;
    public InventorySlots content8;
    public InventorySlots content9;
    public InventorySlots content10;
    public InventorySlots content11;
    public InventorySlots content12;
    public InventorySlots content13;
    public InventorySlots content14;
    public InventorySlots content15;
    public InventorySlots content16;

    public InventoryItem empty;

    public List<InventorySlots> listContent = new List<InventorySlots>();

    private bool hecho;

    //Asigno el texto de la descripción con la descripción correspondiente
    public void SetText(string description)
    {
        descriptionText.text = description;
    }

    
    // Start is called before the first frame update
    //Hago que la pestaña de descripción aparezca vacía al iniciarlo
    void Start()
    {
        SetText("");
    }

    //Por cada objeto que tenga en mi inventario, habrá un slot con su sprite y descripción en el menú de inventario
    //Asigno cada objeto a un slot que haya vacío
    //Cada vez que abro este menú, asigno un objeto vacío para así poder eliminar los objetos que ya he utilizado, por ejemplo una llave, la uso y se va.
    void OnEnable()
    {
        if (!hecho)
        {
            listContent.Add(content);
            listContent.Add(content2);
            listContent.Add(content3);
            listContent.Add(content4);
            listContent.Add(content5);
            listContent.Add(content6);
            listContent.Add(content7);
            listContent.Add(content8);
            listContent.Add(content9);
            listContent.Add(content10);
            listContent.Add(content11);
            listContent.Add(content12);
            listContent.Add(content13);
            listContent.Add(content14);
            listContent.Add(content15);
            listContent.Add(content16);
            hecho = true;
        }
        for(int i=0;i<listContent.Count;i++)
        {
            listContent[i].Setup(empty,this);
        }
        for (int i=0; i<playerInventory.myInventory.Count;i++)
        {
            Debug.Log(playerInventory.myInventory.Count);
            Debug.Log(listContent.Count);
            listContent[i].Setup(playerInventory.myInventory[i], this);
        } 
    }


    //Asigno la descripción
    public void SetupDescription(string newDescriptionString)
    {
        descriptionText.text = newDescriptionString;
    }

   
}
