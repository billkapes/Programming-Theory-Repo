using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text OrderText;
    [SerializeField] GameObject topping1, topping2, topping3;
    private readonly string[] toppings = { "pepperoni", "bacon", "mushroom" };
    public string[] currentPizza = new string[3];
    public string[] currentOrder = new string[3];


    void Start()
    {
        OrderText.text = "";
        GenerateOrder();
    }

    private void GenerateOrder()
    {
        ResetOrder();

        if (Random.Range(0, 2) > 0)
        {
            currentOrder[0] = toppings[0];
            OrderText.text = toppings[0] + "\n";
        }
        if (Random.Range(0, 2) > 0)
        {
            currentOrder[1] = toppings[1];
            OrderText.text += toppings[1] + "\n";
        }
        if (Random.Range(0, 2) > 0)
        {
            currentOrder[2] = toppings[2];
            OrderText.text += toppings[2] + "\n";
        }
    }

    private void ResetOrder()
    {
        for (int i = 0; i < currentOrder.Length; i++)
        {
            currentOrder[i] = "";
        }
        OrderText.text = "";
    }

    void Update()
    {
        
    }

    public void PepperoniButtonPressed()
    {
        currentPizza[0] = "pepperoni";
        topping1.SetActive(true);
    }

    public void BaconButtonPressed()
    {
        currentPizza[1] = "bacon";
        topping2.SetActive(true);

    }
    public void MushroomButtonPressed()
    {
        currentPizza[2] = "mushroom";
        topping3.SetActive(true);

    }

    public void ServeButtonPressed()
    {
        for (int i = 0; i < toppings.Length; i++)
        {
            if (currentOrder[i] != currentPizza[i])
            {
                Debug.Log("invalid pizza");
                ResetPizza();
                return;
            }
        }

        Debug.Log("valid pizza");
        ResetPizza();
    }

    private void ResetPizza()
    {
        topping1.SetActive(false);
        topping2.SetActive(false);
        topping3.SetActive(false);
        for (int i = 0; i < currentPizza.Length; i++)
        {
            currentPizza[i] = "";
        }

        GenerateOrder();
    }
}
