using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip finalBossMusic;

    [SerializeField] float cameraSize;
    [SerializeField] GameObject finalBoss;
    
    [SerializeField] public bool isFinalBoss;


    private bool activeBossFight;
    void Start()
    {

        AudioManager.instance.PlayMusic(levelMusic);

    }

    public void FinalBossWasVanquished()
    {
        AudioManager.instance.PlayMusic(levelMusic);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFinalBoss = true;


        if (!activeBossFight && isFinalBoss && collision.gameObject.tag.Equals(TagId.Player.ToString()))
        {
            activeBossFight = true;

            AudioManager.instance.PlayMusic(finalBossMusic);
           
            if (finalBoss)
            {
                finalBoss.SetActive(true);
               
            }
            FindObjectOfType<CameraController>().ChangeCameraSize(cameraSize);
        }
    }
   
}
