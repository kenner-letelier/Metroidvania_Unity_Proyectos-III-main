using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.IO;

public class puerta : MonoBehaviour
{
    public Animator animPuerta;
    public Animator animPuerta1;

    public GameObject text1;
    public GameObject text2;

    private void Start()
    {
        animPuerta.Play("IdlePuerta");
        animPuerta1.Play("IdlePuerta");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("key"))
        {
            abrirpuerta.llave += 1;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("door") && abrirpuerta.llave == 0)
        {
            text1.SetActive(true);
        }
        if (collision.tag.Equals("door") && abrirpuerta.llave == 1)
        {
            text2.SetActive(true);
            abrir();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("door") && abrirpuerta.llave == 0)
        {
            text1.SetActive(false);
        }
        if (collision.tag.Equals("door") && abrirpuerta.llave == 1)
        {
            text2.SetActive(false);
        }
    }

    public void abrir()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animPuerta.Play("Puerta");
            animPuerta1.Play("Puerta");
        }
            
    }
}
