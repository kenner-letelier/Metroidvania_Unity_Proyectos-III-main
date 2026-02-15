using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pergaminos : MonoBehaviour
{
    public GameObject exitpanel;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            BackButon();
        }
    }
    public void BackButon()
    {

        exitpanel.SetActive(false);
        HeroController.instance.SetIsControlable(true);
    }
}
