using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject exitpanel;
    bool active;

    private void Start()
    {
        active = false;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetButtonDown("Salir"))
        {
            active = true;
            HeroController.instance.SetIsControlable(false);
            exitpanel.SetActive(true);
            Debug.Log("pantalla activa");
        }
       

        if (active == true && Input.GetButtonDown("Submit"))
        {
            YesExit();
        }
        if (active == true && Input.GetButtonDown("Cancel"))
        {
            NoExit();
        }

    }

    public void YesExit()
    {
        SceneHelper.instance.LoadScene(SceneId.TitleScreen);
        Destroy(this.gameObject);
    }
    public void NoExit()
    {

        exitpanel.SetActive(false);
        active = false;
        HeroController.instance.SetIsControlable(true);
    }
}
