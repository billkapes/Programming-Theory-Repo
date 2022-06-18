using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bacon : Topping
{
    public override void Entrance()
    {
        transform.DOMoveX(-3, GameManager.StandardWaitTime).From();
        GetComponent<AudioSource>().Play();
        base.Entrance();
    }

    private void OnEnable()
    {
        Entrance();
    }
}
