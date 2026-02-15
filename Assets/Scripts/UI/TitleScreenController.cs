using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TitleScreenController : MonoBehaviour
{

    public bool showOptions = false;

    public List<GameObject> optionButtons;
    public GameObject pressEnterButton;
    public GameObject continueButton;
    public GameObject controlesButton;


    [SerializeField] AudioClip enterSfx;
    [SerializeField] AudioClip buttonSfx;

    GameModel gameModel;


    GameData gameData;
    private void Awake()
    {
        gameModel = new GameModel();
        SaveSystem.instance.Load(out gameData);

    }

    private void Update()
    {
        if (!showOptions && Input.GetButtonDown("SubmitMenu"))
            {
            showOptions = true;
            pressEnterButton.SetActive(false);
            foreach (var button in optionButtons)
            {
                button.SetActive(true);
            }
            AudioManager.instance.PlaySfx(enterSfx);

            if (gameData != null)
            {
                continueButton.SetActive(true);
            }
        }
    }

    public void ContinueGame()
    {
        AudioManager.instance.PlaySfx(buttonSfx);

        SceneHelper.instance.LoadScene(gameData.levelData.sceneId);


    }
    public void ExitGame()
    {
        AudioManager.instance.PlaySfx(buttonSfx);
        
        Application.Quit();

    }
    public void StartNewGame()
    {
        DeleteSaves();
       
        
        AudioManager.instance.PlaySfx(buttonSfx);

        SceneHelper.instance.LoadScene(SceneId.Level1_1);
    }
    public void Controles()
    {
    
        AudioManager.instance.PlaySfx(buttonSfx);

        SceneHelper.instance.LoadScene(SceneId.Level1_3);
    }
    public void Sinopsis()
    {

        AudioManager.instance.PlaySfx(buttonSfx);

        SceneHelper.instance.LoadScene(SceneId.Level1_4);
    }
    public HeroData Delete(string gameName)
    {

        var path = Application.persistentDataPath + "//HeroData_" + gameName + ".data";
        var path2 = Application.persistentDataPath + "//LevelData_" + gameName + ".data";
        Debug.Log(path);

        if (File.Exists(path) && File.Exists(path2))
        {
            File.Delete(path);
            File.Delete(path2);

        }
        return null;

    }
    public void DeleteSaves()
    {
       // Delete(gameData.gameName);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
