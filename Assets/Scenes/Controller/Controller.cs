using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject BackButton;

    private void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            back();
        }
        if (Input.GetButtonDown("Submit"))
        {
            next();
        }
    }
    public void back()
    {

        SceneHelper.instance.LoadScene(SceneId.TitleScreen);
    }
    public void next()
    {

        panel1.SetActive(true);
        panel2.SetActive(false);
    }
}
