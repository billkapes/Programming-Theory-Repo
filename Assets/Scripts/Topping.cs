using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Topping : MonoBehaviour
{
    public virtual void Entrance()
    {
        transform.DOMoveY(5, 1).From();

    }

    
}
