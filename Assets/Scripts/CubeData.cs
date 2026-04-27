using UnityEngine;

[DisallowMultipleComponent]
public class CubeData : MonoBehaviour
{
    [SerializeField] private int _generation;

    public int Generation 
    { 
        get => _generation; 
        set => _generation = value; 
    }
}