using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mushroom : Topping
{
    public override void Entrance()
    {
        transform.DOMoveX(5, 1).From();
        base.Entrance();
    }

    private void OnEnable()
    {
        Entrance();
    }
}
