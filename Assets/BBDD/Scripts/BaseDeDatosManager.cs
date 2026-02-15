using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;


public class BaseDeDatosManager : MonoBehaviour
{

    public GameObject CoinsAmountPrefab;
    public GameObject CronometroPrefab;
    public GameObject Panel;
    public Transform CoinsParent;
    public Transform CronometroParent;

    private List<ShowData> showDatas = new List<ShowData>();

    private string connectionString;

    public bool isDead;
    public bool canAdd;
    public bool Continue;

    public int Coins;
    public int saveAmount;
    public int Cronometro;

    private int contador;
    private int maxCount = 1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
        }
        
        connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/EclipseDB.sqlite";
        CreateTable();

    }
    public static BaseDeDatosManager _instance;

    public static BaseDeDatosManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BaseDeDatosManager>();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    private void Start()
    {
        isDead = false;
        canAdd = false;
        Continue = false;

        CoinsCounter BDVariables = GetComponent<CoinsCounter>();
        Temporizador temporizador = GetComponent<Temporizador>();


        Coins = BDVariables.newAmount;
        Cronometro = temporizador.TiempoFinal;

        connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/EclipseDB.sqlite";
        DeleteCoin(1);
        DeleteTime(1);
   

    }

    private void Update()
    {

        switch (isDead)
        {
            case true:
                Panel.SetActive(true); break;
            default:
                Panel.SetActive(false); break;
        }

        UpdateData();
        
        
    }

    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                //string sqlQuery = String.Format("CREATE TABLE if not exists Coins(ID INTEGER, CantidadInt INTEGER, PRIMARY KEY(ID))");
                string sqlQuery = String.Format("CREATE TABLE if not exists 'Coins'('ID' INTEGER, 'CantidadInt' INTEGER, PRIMARY KEY('ID'))");
                string sqlQuery2 = String.Format("CREATE TABLE if not exists 'Tiempo'('ID', 'Cronometro' INTEGER, PRIMARY KEY('ID'))");




                dbCmd.CommandText = sqlQuery;
                dbCmd.CommandText = sqlQuery2;
                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }
    private void AddCoins(int newCoin)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Coins(CantidadInt) VALUES(\"{0}\")", newCoin);
                


                dbCmd.CommandText = sqlQuery;
                
                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }

    private void AddTime(int newTime)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Tiempo(Cronometro) VALUES(\"{0}\")", newTime);


                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }

    private void DeleteCoin(int id)
    {
        
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("DELETE FROM Coins WHERE ID = \"{0}\"", id);
                dbCmd.CommandText = sqlQuery;


                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }
    private void DeleteTime(int id)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = String.Format("DELETE FROM Tiempo WHERE ID = \"{0}\"", id);
                dbCmd.CommandText = sqlQuery;

                dbCmd.ExecuteScalar();
                dbConnection.Close();

            }
        }
    }


    private void MostrarMonedas()
    {
        showDatas.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Coins";
                

                dbCmd.CommandText = sqlQuery;
                

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        showDatas.Add(new ShowData(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(1)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }
    private void MostrarTiempo()
    {
        showDatas.Clear();
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                
                string sqlQuery = "SELECT * FROM Tiempo";

                dbCmd.CommandText = sqlQuery;


                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        showDatas.Add(new ShowData(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(1)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }



    //esto muestra en pantalla las monedas recogidas junto con el CoinsScript.
    private void ShowCoinsData()
    {

        MostrarMonedas();
    
        
        for (int i = 0; i < showDatas.Count; i++)
        {
            GameObject tmpObject = Instantiate(CoinsAmountPrefab);
            ShowData coinsData = showDatas[i];
            tmpObject.GetComponent<CoinsScript>().SetCoins(coinsData.Coins.ToString());
            //tmpObject.GetComponent<CoinsScript>().SetId(coinsData.ID.ToString());
            tmpObject.transform.SetParent(CoinsParent);
           

        }
        
    }
    private void ShowTimeData()
    {

        MostrarTiempo();


        for (int i = 0; i < showDatas.Count; i++)
        {
            GameObject tmpObject = Instantiate(CronometroPrefab);
            ShowData cronoData = showDatas[i];
            tmpObject.GetComponent<CronoScript>().SetCrono(cronoData.Cronometro.ToString());
            //tmpObject.GetComponent<CoinsScript>().SetId(coinsData.ID.ToString());
            tmpObject.transform.SetParent(CronometroParent);


        }

    }

    private void DeleteExtraCoins()
    {
        MostrarMonedas();
        if (saveAmount <= showDatas.Count)
        {
            int deleteCount = showDatas.Count - saveAmount;
            showDatas.Reverse();
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
            
                dbConnection.Open();
                for (int i = 0; i < deleteCount; i++)
                {
                    using (IDbCommand dbCmd = dbConnection.CreateCommand())
                    {
                        string sqlQuery = String.Format("DELETE FROM Coins WHERE ID = \"{0}\"", showDatas[i].ID);


                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();
                       

                    }
                    dbConnection.Close();
                }
                
            }
        }
    }
    private void DeleteExtraTimers()
    {
        MostrarTiempo();
        if (saveAmount <= showDatas.Count)
        {
            int deleteCount = showDatas.Count - saveAmount;
            showDatas.Reverse();
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {

                dbConnection.Open();
                for (int i = 0; i < deleteCount; i++)
                {
                    using (IDbCommand dbCmd = dbConnection.CreateCommand())
                    {
                        string sqlQuery = String.Format("DELETE FROM Tiempo WHERE ID = \"{0}\"", showDatas[i].ID);


                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();


                    }
                    dbConnection.Close();
                }

            }
        }
    }

    private void UpdateData()
    {
        HeroController heroController = GetComponent<HeroController>();
        Temporizador temporizador = GetComponent<Temporizador>();
        CoinsCounter BDVariables = GetComponent<CoinsCounter>();


       

        Cronometro = temporizador.TiempoFinal;
        isDead = heroController.isDead;
        Coins = BDVariables.newAmount;

        

        if (Coins > 0 && Cronometro > 0) { canAdd = true; }
        else { canAdd = false; }

        if (isDead == true && canAdd == true)
        {
            for (int i = contador; i < maxCount; i++)
            {
                contador = contador + 1;
                
                    Debug.Log("el valor de Coins es: " + Coins);

                
                AddCoins(Coins);
                AddTime(Cronometro);
                
                DeleteExtraCoins();
                DeleteExtraTimers();
                ShowCoinsData();
                ShowTimeData();

            }
        }
       
 
        

    }

   

}
