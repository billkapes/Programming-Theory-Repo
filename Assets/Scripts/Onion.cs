using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Onion : Topping
{
    public override void Entrance()
    {
        transform.DOMoveX(3, 1).From();
        GetComponent<AudioSource>().Play();
        base.Entrance();
    }

    private void OnEnable()
    {
        Entrance();
    }
}
