using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este script es para crear un tipo de objeto desde el propio unity que almacenar� sus propios datos

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 defaultValue;

    //Tendr� 2 Vector2, y defaultValue ser� igual que initialValue
    public void OnAfterDeserialize() { initialValue = defaultValue; }
    public void OnBeforeSerialize() { }
    
}
