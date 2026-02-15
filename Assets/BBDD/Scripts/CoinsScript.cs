using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsScript : MonoBehaviour
{

    public GameObject coinsAmount;
 

    public void SetCoins(string coins)
    {
        this.coinsAmount.GetComponent<Text>().text = coins;
    }



    /*public void SetId(string id)
    {
        this.coinsAmount.GetComponent<Text>().text = id;
    }*/
}

