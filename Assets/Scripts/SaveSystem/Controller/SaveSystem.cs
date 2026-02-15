using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    [Header("Debug")]
    public bool save;
    public bool load;


    [SerializeField] GameData gameData;
    [SerializeField] HeroData heroData;
    [SerializeField] LevelData levelData;


    private const string gameName = "Eclipse";

    private GameModel gameModel = new GameModel();
    private LevelModel levelModel = new LevelModel();
    private HeroModel heroModel = new HeroModel();


    private static SaveSystem _instance;



    public static SaveSystem instance
    {
        get
        {




            if (_instance == null)
            {

                _instance = FindObjectOfType<SaveSystem>();

                GameObject gameO = null;
                if (_instance == null)
                {
                    gameO = new GameObject("SaveSystem");
                    gameO.AddComponent<SaveSystem>();
                    _instance = gameO.GetComponent<SaveSystem>();

                }
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;

        }

    }



    public void Save(GameData data)
    {
        data.gameName = gameName;
        gameData = data;
        gameModel.Save(data);
    }

    public void Save(LevelData data)
    {
        levelData = data;

        levelModel.Save(gameName, data);
    }
    public void Save(HeroData data)
    {
        heroData = data;

        heroModel.Save(gameName, data);
    }

    public void Load(out GameData data)
    {
        data = new GameData();
        data.gameName = gameName;
        data = gameModel.Load(data);
        if (data.levelData == null && data.heroData == null)
        {
            data = null;
        }
        gameData = data;
    }

    public void Load(out LevelData data)
    {
        data = new LevelData();
        data = levelModel.Load(gameName);
        levelData = data;
    }
    public void Load(out HeroData data)
    {
        data = new HeroData();
        data = heroModel.Load(gameName);
        heroData = data;
    }

    private void Update()
    {
        if (save)
        {
            save = false;
            Save(gameData);

        }
        if (load)
        {
            load = false;
            Load(out gameData);


        }
    }

}
