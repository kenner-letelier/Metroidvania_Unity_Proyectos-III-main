using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISaveGameScreen
{
    void OnHideScreen();
}

public class SaveGameScreen : MonoBehaviour
{

    private static SaveGameScreen _instance;
    [SerializeField] CanvasFade canvasFade;
    public static SaveGameScreen instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveGameScreen>();
                if (_instance == null)
                {
                    var go = Resources.Load("UI/SaveGameScreen") as GameObject;
                    go = Instantiate(go, Vector3.zero, Quaternion.identity);
                    _instance = go.GetComponent<SaveGameScreen>();

                    var eventSystem = FindObjectOfType<EventSystem>();
                    if (eventSystem == null)
                    {
                        var g = new GameObject("EventSystem");
                        g.AddComponent<EventSystem>();
                        g.AddComponent<StandaloneInputModule>();

                    }

                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
    private ISaveGameScreen saveGameScreen;
    public void ShowScreen(ISaveGameScreen saveGameScreen)
    {
        this.saveGameScreen = saveGameScreen;
        canvasFade.FadeIn();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SaveGame();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            NoSave();
        }
    }

    public void SaveGame()
    {
        
        GameManager.instance.SaveGame();
        StartCoroutine(_SaveGame());
        
        Debug.Log("Savegame");

    }

    private IEnumerator _SaveGame()
    {
        yield return canvasFade._FadeOut();
        this.saveGameScreen.OnHideScreen();

        
    }
    public void NoSave()
    {
        StartCoroutine(_NoSave());
        

    }
    private IEnumerator _NoSave()
    {
        yield return canvasFade._FadeOut();
        this.saveGameScreen.OnHideScreen();

        Destroy(this.gameObject);
    }
}
