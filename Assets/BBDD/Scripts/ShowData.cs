using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowData
{
    [SerializeField] TagId playerTag;

    public int Coins { get; set; }
    public int ID { get; set; }

    public int Cronometro { get; set; }

    public ShowData(int id, int coins, int cronometro)
    {
        this.ID = id;
        this.Coins = coins;
        this.Cronometro = cronometro;
    }
    
}
