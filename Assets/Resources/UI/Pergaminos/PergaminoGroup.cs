using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PergaminoGroup : MonoBehaviour
{
    public GameObject pergamino;
    [SerializeField] TagId playerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(playerTag.ToString()))
        {
            pergamino.SetActive(true);
            HeroController.instance.SetIsControlable(false);
            Destroy(this.gameObject);
        }
    }
}
