using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{


    private static LoadingScreen _instance;
    [SerializeField] CanvasFade canvasFade;
    public static LoadingScreen instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LoadingScreen>();
                if (_instance == null)
                {
                    var go = Resources.Load("UI/LoadingScreen") as GameObject;
                    go = Instantiate(go, Vector3.zero, Quaternion.identity);
                    _instance = go.GetComponent<LoadingScreen>();

                }
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void Awake()
    {
        GetComponent<Canvas>().sortingLayerName = "UI";
    }

    public void OnLoadScreen()
    {
        canvasFade.FadeIn();
    }


    public void OnLoadedScreen()
    {
        canvasFade.FadeOut();

    }




    public IEnumerator _OnLoadScreen()
    {
        yield return canvasFade._FadeIn();
    }


    public IEnumerator _OnLoadedScreen()
    {
        yield return canvasFade._FadeOut();
        Destroy(this.gameObject);
    }

}
