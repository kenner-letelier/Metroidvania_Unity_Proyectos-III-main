using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CronoScript : MonoBehaviour
{

    public GameObject TimeData;

   

    public void SetCrono(string Timer)
    {
        this.TimeData.GetComponent<Text>().text = Timer;
    }

}
