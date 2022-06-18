using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Este script sirve para cambiar el n�mero de monedas que aparece en la interfaz, igualandolo al n�mero de monedas del inventario
public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    private void Start()
    {
        coinDisplay.text = "0";
    }

    public void UpdateCoinCount()
    {
        
        coinDisplay.text = "" + playerInventory.coins;
    }
}
