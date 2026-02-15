using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    public int amount;
    public int newAmount;
    public bool isDead;
    

    
    

    private void Awake()
    {
       
        isDead = false;
    }
   private void Update()
    {
        HeroController heroController = GetComponent<HeroController>();
        isDead = heroController.isDead;
        updateAmount();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            amount = amount + 1;
           
           
            Debug.Log("Tiene: " + amount + "monedas");
        }
    }

    public void updateAmount()
    {
        if (isDead == true)
        {
            newAmount = amount;
            
        }
    }
        
   
   
    

}
