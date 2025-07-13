using System;
using UnityEngine;

public class SolvableMuzzle : MonoBehaviour
{
    public event Action IsMuzzleSolved;
    
    public void OnPlayerInvoke()
    {
        IsMuzzleSolved?.Invoke();        
    }
}
