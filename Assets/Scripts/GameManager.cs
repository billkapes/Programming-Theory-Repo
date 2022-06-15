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
    [SerializeField] GameObject topping1, topping2, topping3, topping4, topping5;
    [SerializeField] GameObject thePizza;
    [SerializeField] GameObject gameOverText;
    [SerializeField] AudioClip correct, incorrect, swish, printer, whoosh, fail;

    readonly string[] toppings = { "pepperoni", "bacon", "mushroom", "onion", "pineapple" };
    readonly float screenEdgeWidth = 9;

    string[] currentPizza = new string[5];
    string[,] orderValues = new string[5,5];
    float inputWaitTimer, orderGenerateTime;
    int orderCount, totalServedCount;
    int orderIndex;
    
    void Start()
    {
        for (int i = 0; i < currentPizza.Length; i++)
        {
            currentPizza[i] = "";
        }

        inputWaitTimer = 1;
        orderGenerateTime = 6;

        InitOrderValues();

        GetComponent<AudioSource>().PlayOneShot(swish);
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
            for (int j = 0; j < 5; j++)
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
            if (orderCount % 3 == 0)
            {
                orderGenerateTime = Mathf.Max(orderGenerateTime - 1, 4);
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
            GetComponent<AudioSource>().PlayOneShot(printer);
        }
        else
        {
            //game over
            GetComponent<AudioSource>().PlayOneShot(fail);
            StopAllCoroutines();
            inputWaitTimer = 5;
            DOTween.KillAll();
            gameOverText.SetActive(true);
            totalServedText.text += " " + totalServedCount;
            totalServedText.gameObject.SetActive(true);
            Invoke(nameof(GoBackToTitle), 4);
        }

        //make random order
        orderText[orderIndex].text = "1 Pizza\nToppings:\n";
        for (int i = 0; i < 5; i++)
        {
            if (Random.Range(0, 2) > 0)
            {
                orderValues[orderIndex, i] = toppings[i];
                orderText[orderIndex].text += "\t-" + toppings[i] + "\n";
            }
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

    public void OnionButtonPressed()
    {
        if (inputWaitTimer > 0f)
        {
            return;
        }
        currentPizza[3] = toppings[3];
        topping4.SetActive(true);
        inputWaitTimer = 1;
    }

    public void PineappleButtonPressed()
    {
        if (inputWaitTimer > 0f)
        {
            return;
        }
        currentPizza[4] = toppings[4];
        topping5.SetActive(true);
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
                        GetComponent<AudioSource>().PlayOneShot(swish);
                        GetComponent<AudioSource>().PlayOneShot(correct);
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
        GetComponent<AudioSource>().PlayOneShot(incorrect);
        ResetPizza();
        inputWaitTimer = 1;
    }

    void FinishServe()
    {
        thePizza.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ResetPizza();
        GetComponent<AudioSource>().PlayOneShot(swish);
        thePizza.transform.GetChild(0).DOMoveX(-screenEdgeWidth, 0.5f).From();
        thePizza.transform.GetChild(1).DOMoveX(-screenEdgeWidth, 0.75f).From();
        thePizza.transform.GetChild(2).DOMoveX(-screenEdgeWidth, 1f).From();
    }

    private void ResetPizza()
    {
        topping1.SetActive(false);
        topping2.SetActive(false);
        topping3.SetActive(false);
        topping4.SetActive(false);
        topping5.SetActive(false);
        for (int i = 0; i < currentPizza.Length; i++)
        {
            currentPizza[i] = "";
        }
    }

    private void ResetOrder(int index)
    {
        for (int i = 0; i < 5; i++)
        {
            orderValues[index, i] = "";
        }
        orderText[index].text = "";
        panels[index].SetActive(false);
    }
}
