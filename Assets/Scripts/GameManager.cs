using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] Text[] orderText;
    [SerializeField] Text totalServedText;
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject topping1, topping2, topping3;
    [SerializeField] GameObject thePizza;
    [SerializeField] GameObject gameOverText;

    readonly string[] toppings = { "pepperoni", "bacon", "mushroom" };
    readonly float screenEdgeWidth = 9;

    string[] currentPizza = new string[3];
    string[,] orderValues = new string[5,3];
    float inputWaitTimer, orderGenerateTime;
    int orderCount, totalServedCount;
    int orderIndex;
    
    void Start()
    {
        currentPizza[0] = "";
        currentPizza[1] = "";
        currentPizza[2] = "";

        inputWaitTimer = 1;
        orderGenerateTime = 6;

        InitOrderValues();

        thePizza.transform.GetChild(0).DOMoveX(-screenEdgeWidth, 0.5f).From();
        thePizza.transform.GetChild(1).DOMoveX(-screenEdgeWidth, 0.75f).From();
        thePizza.transform.GetChild(2).DOMoveX(-screenEdgeWidth, 1f).From();

        Invoke(nameof(StartOrders), 1);
    }

    void StartOrders()
    {
        StartCoroutine(OrderTimer());
    }

    void Update()
    {
        if (inputWaitTimer >= 0)
        {
            inputWaitTimer -= Time.deltaTime;
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

    IEnumerator OrderTimer()
    {
        for(; ; )
        {
            GenerateOrder();
            orderCount++;
            if (orderCount % 4 == 0)
            {
                orderGenerateTime = Mathf.Max(orderGenerateTime - 1, 3);
            }
            yield return new WaitForSeconds(orderGenerateTime);
        }
    }

    private void GenerateOrder()
    {
        //search for unused panel or else game over
        int potentialIndex = FindUnusedPanelIndex();
        if (potentialIndex < 5)
        {
            orderIndex = potentialIndex;
            panels[orderIndex].SetActive(true);
            orderText[orderIndex].text = "";
        }
        else
        {
            StopAllCoroutines();
            gameOverText.SetActive(true);
            totalServedText.text += " " + orderCount;
            totalServedText.gameObject.SetActive(true);
            Invoke(nameof(GoBackToTitle), 3);
        }

        //make random order
        orderText[orderIndex].text = "1 Pizza\nToppings:\n";
        if (Random.Range(0, 2) > 0)
        {
            orderValues[orderIndex, 0] = toppings[0];
            orderText[orderIndex].text += "\t-" + toppings[0] + "\n";
        }
        if (Random.Range(0, 2) > 0)
        {
            orderValues[orderIndex, 1] = toppings[1];
            orderText[orderIndex].text += "\t-" + toppings[1] + "\n";
        }
        if (Random.Range(0, 2) > 0)
        {
            orderValues[orderIndex, 2] = toppings[2];
            orderText[orderIndex].text += "\t-" + toppings[2] + "\n";
        }
    }

    void GoBackToTitle()
    {
        SceneManager.LoadScene(0);
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
        if (inputWaitTimer > 0f)
        {
            return;
        }
        currentPizza[0] = toppings[0];
        topping1.SetActive(true);
        inputWaitTimer = 1;
    }

    public void BaconButtonPressed()
    {
        if (inputWaitTimer > 0f)
        {
            return;
        }
        currentPizza[1] = toppings[1];
        topping2.SetActive(true);
        inputWaitTimer = 1;

    }
    public void MushroomButtonPressed()
    {
        if (inputWaitTimer > 0f)
        {
            return;
        }
        currentPizza[2] = toppings[2];
        topping3.SetActive(true);
        inputWaitTimer = 1;
    }

    public void ServeButtonPressed()
    {
        if (inputWaitTimer > 0f)
        {
            return;
        }

        //check for each active order
        for (int i = 0; i < 5; i++)
        {
            if (!panels[i].activeInHierarchy)
            {
                continue;
            }
            //checking toppings
            for (int j = 0; j < toppings.Length; j++)
            {
                if (orderValues[i, j] == currentPizza[j])
                {
                    if (j == toppings.Length - 1)
                    {
                        //valid order for pizza found
                        totalServedCount++;
                        ResetOrder(i);
                        thePizza.transform.DOMoveX(screenEdgeWidth, 1).OnComplete(FinishServe);
                        inputWaitTimer = 2;
                        return;
                    }
                    continue;
                }
                else  
                    break;
            }
        }
        // no valid order found
        ResetPizza();
        inputWaitTimer = 1;
    }

    void FinishServe()
    {
        thePizza.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ResetPizza();
                       
        thePizza.transform.GetChild(0).DOMoveX(-screenEdgeWidth, 0.5f).From();
        thePizza.transform.GetChild(1).DOMoveX(-screenEdgeWidth, 0.75f).From();
        thePizza.transform.GetChild(2).DOMoveX(-screenEdgeWidth, 1f).From();
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
        orderText[index].text = "";
        panels[index].SetActive(false);
    }
}
