using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
   public static SaveController instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LoadProgress();
    }

    public static void SaveProgress()
    {
        SaveObject so = new SaveObject();

        so.coins = Inventory.currentCoins;
        so.highscore = UIController.GetHighScore();
        so.shipStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>().shipStats;

        SaveAndLoad.SaveState(so);
    }

    public static void LoadProgress()
    {
        SaveObject so = SaveAndLoad.LoadState();

        Inventory.currentCoins = so.coins;
        UIController.UpdateHighScore(so.highscore);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>().shipStats = so.shipStats;
    }
}
