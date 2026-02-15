using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour, ISaveGameScreen
{
    private bool screenIsShowing;

    public void OnHideScreen()
    {
        screenIsShowing = false;
        HeroController.instance.SetIsControlable(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(TagId.Player.ToString()) && !screenIsShowing)
        {
            SaveGameScreen.instance.ShowScreen(this);
            HeroController.instance.SetIsControlable(false);
            screenIsShowing = true;
        }
    }
}