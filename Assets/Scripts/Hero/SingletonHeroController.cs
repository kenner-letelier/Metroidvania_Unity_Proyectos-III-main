using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonHeroController : MonoBehaviour
{

// Si el objeto del Hero no es compartido entre las escenas 
//COPIAR Y PEGAR  el siguiente codigo en el HeroController
    
    /*

    public static HeroController _instance;

    public static HeroController instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<HeroController>();
            }
            return _instance;
        }
        set {
            _instance = value;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        if(instance&& instance!=this) {
            Destroy(this.gameObject);
        }


    }
    */

}
