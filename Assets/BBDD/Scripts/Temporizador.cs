using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Temporizador : MonoBehaviour
{

    public static Temporizador instance;
    public Text Crono;
    private TimeSpan TimeCrono;
    private bool timerBool;
    public float currentTime;
    public int TiempoFinal;
    

    public bool isDead;


    private void Awake()
    {
        instance = this;
        

    }

    private void Start()
    {
        
        Crono.text = " Tiempo 00:00";
        timerBool = false;
        IniciarTiempo();

    }

    private void Update()
    {
        HeroController heroController = GetComponent<HeroController>();
        isDead = heroController.isDead;
        
        FinTiempo();
        

        
    }
    public void IniciarTiempo()
    {
        timerBool = true;
        currentTime = 0F;

        StartCoroutine(AcUpdate());
       
    }

    public void FinTiempo()
    {
        if (isDead == true)
        {
            timerBool = false;
            TiempoFinal = Convert.ToInt32(currentTime);
        }
        

    }

    private IEnumerator AcUpdate()
    {
        while (timerBool)
        {
            currentTime += Time.deltaTime;
            TimeCrono = TimeSpan.FromSeconds(currentTime);
            string tiempoCronoStr = "Tiempo: " + TimeCrono.ToString("mm':'ss':'ff");
            Crono.text = tiempoCronoStr;

            yield return null;
        }
    }
}
