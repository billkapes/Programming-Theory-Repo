using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// INHERITANCE
public abstract class Topping : MonoBehaviour
{
    public virtual void Entrance()
    {
        transform.DOMoveY(5, GameManager.StandardWaitTime).From();
        transform.DOMoveZ(-5, GameManager.StandardWaitTime).From();        
    }

    
}
