using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{

    private static SceneHelper _instance;


    public SceneId previousScene;
    public static SceneHelper instance
    {
        get
        {

            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneHelper>();

                if (_instance == null)
                {
                    var go = new GameObject("SceneHelper");
                    go.AddComponent<SceneHelper>();

                    _instance = go.GetComponent<SceneHelper>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public SceneId GetCurrentSceneId()
    {
        Enum.TryParse(SceneManager.GetActiveScene().name, out SceneId sceneId);
        return sceneId;
    }
    public void ReloadScene()
    {
        Enum.TryParse(SceneManager.GetActiveScene().name, out SceneId sceneId);
        StartCoroutine(_LoadScene(sceneId));
    }

    public void LoadScene(SceneId sceneId)
    {

        StartCoroutine(_LoadScene(sceneId));
    }

    private IEnumerator _LoadScene(SceneId sceneId)
    {
        //  SceneManager.GetActiveScene().name
        yield return LoadingScreen.instance._OnLoadScreen();

        Enum.TryParse(SceneManager.GetActiveScene().name, out previousScene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId.ToString());
        if (Camera.main.GetComponent<CameraController>() != null)
        {
            Camera.main.GetComponent<CameraController>().FreezeCamera();
        }
        if (HeroController.instance != null)
        {
            HeroController.instance.PutOutBoundaries();
        }
        while (!asyncLoad.isDone)
        {
            yield return null;
        }


        var list = FindObjectsOfType<PortalScene>().ToList();
        if (list != null)
        {
            try
            {
                var spawnPosition = list.Find(x => x.SceneToLoad() == previousScene).GetSpawnPosition();
                Debug.Log("spawnPosition " + spawnPosition);
                HeroController.instance.PutOnSpawnPosition(spawnPosition);
                Camera.main.GetComponent<CameraController>().UpdatePosition(spawnPosition);
            }
            catch (Exception ex)
            {
            }

        }


        yield return new WaitForSeconds(1);
        yield return LoadingScreen.instance._OnLoadedScreen();


    }
}
