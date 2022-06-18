using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este método sirve para habilitar e inhabilitar el bocadillo con "?" que aparece cuando puedes interactuar con algo
public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;

    public void Enable()
    {
        contextClue.SetActive(true);
    }
    public void Disable()
    {
        contextClue.SetActive(false);
    }
}
