using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
//Este script es para crear un tipo de objeto desde el propio unity que almacenará sus propios datos


public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    
    [HideInInspector]
    public float RuntimeValue;
    public void OnBeforeSerialize()
    {

    }

    //Cuando se ejecute este método, el valor actual pasará a ser el inicial
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
}
