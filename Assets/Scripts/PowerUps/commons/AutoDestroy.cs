using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float delay = 3;
    [SerializeField] AudioClip Sfx;

    private void Awake()
    {
        if (Sfx)
        {
            AudioManager.instance.PlaySfx(Sfx);
        }
        Destroy(this.gameObject, delay);
    }
}
