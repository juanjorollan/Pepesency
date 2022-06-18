using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script es para crear un tipo de objeto desde el propio unity que almacenará sus propios datos

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 defaultValue;

    //Tendré 2 Vector2, y defaultValue será igual que initialValue
    public void OnAfterDeserialize() { initialValue = defaultValue; }
    public void OnBeforeSerialize() { }
    
}
