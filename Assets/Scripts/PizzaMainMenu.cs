using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad < 1f)
        {
            return;
        }

        transform.Translate(Vector3.right * 5 * Time.deltaTime);
        if (transform.position.x > 10f)
        {
            transform.SetPositionAndRotation(new Vector3(-11, 2, 0), transform.rotation);
        }
    }
}
