using System;
using UnityEngine;

[RequireComponent(typeof(Muzzle))]

public class SolvableMuzzle : MonoBehaviour
{
    public event Action IsMuzzleSolved;
    
    public void OnPlayerInvoke()
    {
        IsMuzzleSolved?.Invoke();        
    }
}
