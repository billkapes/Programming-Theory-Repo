using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pepperoni : Topping
{
// INHERITANCE
    public override void Entrance()
    {
        transform.DOMoveX(-5, GameManager.StandardWaitTime).From();
        GetComponent<AudioSource>().Play();
        base.Entrance();
    }

    private void OnEnable()
    {
        Entrance();
    }
}
