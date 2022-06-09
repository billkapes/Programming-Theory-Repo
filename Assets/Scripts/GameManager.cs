using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    int orderIndex;
    [SerializeField] Text[] OrderText;
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject topping1, topping2, topping3;
    [SerializeField] GameObject thePizza;

    private readonly string[] toppings = { "pepperoni", "bacon", "mushroom" };
    public string[] currentPizza = new string[3];
    //public string[] currentOrder = new string[3];
    public string[,] orderValues = new string[5,3];
    float waitTimer;
    


    void Start()
    {
        currentPizza[0] = "";
        currentPizza[1] = "";
        currentPizza[2] = "";
        waitTimer = 1;
        //OrderText[0].text = "";
        orderIndex = 0;
        InitOrderValues();
      
       
    }

    void Update()
    {
        if (waitTimer >= 0)
        {
            waitTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateOrder();
        }
    }

    void InitOrderValues()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                orderValues[i, j] = "";
            }
        }
    }

    private void GenerateOrder()
    {
        //search for unused panel
        int potentialIndex = FindUnusedPanelIndex();
        if (potentialIndex < 5)
        {
            orderIndex = potentialIndex;
            panels[orderIndex].SetActive(true);
            OrderText[orderIndex].text = "";

        }
        else
        {
            //game over goes here
        }

        //make random order
        if (Random.Range(0, 2) > 0)
        {
            orderValues[orderIndex, 0] = toppings[0];
            OrderText[orderIndex].text = toppings[0] + "\n";
        }
        if (Random.Range(0, 2) > 0)
        {
            orderValues[orderIndex, 1] = toppings[1];
            OrderText[orderIndex].text += toppings[1] + "\n";
        }
        if (Random.Range(0, 2) > 0)
        {
            orderValues[orderIndex, 2] = toppings[2];
            OrderText[orderIndex].text += toppings[2] + "\n";
        }
    }

    int FindUnusedPanelIndex()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (!panels[i].activeInHierarchy)
            {
                return i;
            }           
        }
        return 5;
    }

    public void PepperoniButtonPressed()
    {
        if (waitTimer > 0f)
        {
            return;
        }
        currentPizza[0] = toppings[0];
        //currentPizza[0] = "pepperoni";
        topping1.SetActive(true);
        waitTimer = 1;
    }

    public void BaconButtonPressed()
    {
        if (waitTimer > 0f)
        {
            return;
        }
        currentPizza[1] = toppings[1];
        //currentPizza[1] = "bacon";
        topping2.SetActive(true);
        waitTimer = 1;

    }
    public void MushroomButtonPressed()
    {
        if (waitTimer > 0f)
        {
            return;
        }
        currentPizza[2] = toppings[2];
        //currentPizza[2] = "mushroom";
        topping3.SetActive(true);
        waitTimer = 1;
    }

    public void ServeButtonPressed()
    {
        if (waitTimer > 0f)
        {
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            if (!panels[i].activeInHierarchy)
            {
                continue;
            }
        //check toppings
            for (int j = 0; j < toppings.Length; j++)
            {
                Debug.Log("checking p " + currentPizza[j]);
                Debug.Log("checking o " + orderValues[i, j]);
                if (orderValues[i, j] == currentPizza[j])
                {
                    if (j == toppings.Length - 1)
                    {
                        Debug.Log("valid pizza");
                        ResetOrder(i);
                        thePizza.transform.DOMoveX(15, 1).OnComplete(FinishServe);
                        waitTimer = 1;
                        return;
                    }
                    continue;
                }
                else  
                    break;
            }

        }
        Debug.Log("no valid found");
        ResetPizza();

    }

    void FinishServe()
    {
        thePizza.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ResetPizza();
                
        
        thePizza.transform.DOMoveX(-15, 1).From();
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
    }

    private void ResetOrder(int index)
    {
        for (int i = 0; i < 3; i++)
        {
            orderValues[index, i] = "";
        }
        OrderText[index].text = "";
        panels[index].SetActive(false);
    }
}
