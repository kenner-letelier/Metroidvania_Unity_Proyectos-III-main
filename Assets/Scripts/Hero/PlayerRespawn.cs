using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private float CheckPointPositionX, CheckPointPositionY;
    
    



    private void Awake()
    {
        //DeleteSaves();
    }
    private void Start()
    {

       
        if (PlayerPrefs.GetFloat("CheckPointPositionX") != 0)
        {
            transform.position = (new Vector2(PlayerPrefs.GetFloat("CheckPointPositionX"), PlayerPrefs.GetFloat("CheckPointPositionY")));
        }

    }


    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }




    public void ReachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("CheckPointPositionX", x);
        PlayerPrefs.SetFloat("CheckPointPositionY", y);
    }

}
