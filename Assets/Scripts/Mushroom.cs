using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// INHERITANCE
public class Mushroom : Topping
{
    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
        Entrance();
    }
}
