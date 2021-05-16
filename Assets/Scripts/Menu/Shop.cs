using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class Shop : MonoBehaviour
{
    //public AudioClip noSale;
    //public AudioClip sale;

    public TextMeshProUGUI currentCoins;
    public TextMeshProUGUI healthVal;
    public TextMeshProUGUI fireRateVal;
    public TextMeshProUGUI healthCost;
    public TextMeshProUGUI fireRateCost;

    public Button healthButton;
    public Button fireRateButton;

    private int currentMaxHealth;
    private float currentFireRate;

    private int nextHealthCost;
    private int nextFireRateCost;

    private int maxHealthMultiplier = 5;
    private int fireRateMultiplier = 5;

    private int maxHealthBaseCost = 10;
    private int fireRateBaseCost = 5;

    private Ship player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();

        currentMaxHealth = player.shipStats.maxHealth;
        currentFireRate = player.shipStats.fireRate;
        currentCoins.text = Inventory.currentCoins + "G";

        UpdateUIandTotals();
    }

    public void BuyHealth()
    {
        if (PriceCheck(nextHealthCost))
        {
            Inventory.currentCoins -= nextHealthCost;
            currentCoins.text = Inventory.currentCoins + "G";

            player.shipStats.maxHealth++;
            currentMaxHealth = player.shipStats.maxHealth;

            SaveController.SaveProgress();
            UpdateUIandTotals();

            //AudioController.PlaySoundEffect(sale);
        }
        else
        {
           //AudioController.PlaySoundEffect(noSale);
        }
    }

    public void BuyFireRate()
    {
        if (PriceCheck(nextFireRateCost))
        {
            Inventory.currentCoins -= nextFireRateCost;
            currentCoins.text = Inventory.currentCoins + "G";

            player.shipStats.fireRate -= 0.1f;
            currentFireRate = player.shipStats.fireRate;

            SaveController.SaveProgress();
            UpdateUIandTotals();

            //AudioController.PlaySoundEffect(sale);
        }
        else
        {
            //AudioController.PlaySoundEffect(noSale);
        }
    }

    private void UpdateUIandTotals()
    {
        if(currentMaxHealth < 5)
        {
            nextHealthCost = currentMaxHealth * (maxHealthBaseCost * maxHealthMultiplier);
            healthVal.text = currentMaxHealth + " => " + (currentMaxHealth + 1);
            healthCost.text = nextHealthCost + "G";
            healthButton.interactable = true;
        }
        else
        {
            healthCost.text = "MAX";
            healthVal.text = currentMaxHealth.ToString();
            healthButton.interactable = false;
        }

        if(currentFireRate > 0.2f)
        {
            nextFireRateCost = 0;

            for(float f = 1; f > 0.2f; f -= 0.1f)
            {
                nextFireRateCost += (fireRateBaseCost * fireRateMultiplier);

                if (f <= currentFireRate)
                    break;
            }

            fireRateVal.text = currentFireRate.ToString("0.00") + " => " + (currentFireRate - 0.1f).ToString("0.00");
            fireRateCost.text = nextFireRateCost + "G";
            fireRateButton.interactable = true;
        }
        else
        {
            fireRateCost.text = "MAX";
            fireRateVal.text = "0.20";
            fireRateButton.interactable = false;
        }
    }

    private bool PriceCheck(int cost)
    {
        if (Inventory.currentCoins >= cost)
            return true;
        else
            return false;
    }

#if UNITY_EDITOR
    [MenuItem("Cheats/Add Coins")]
    private static void AddCoinCheat()
    {
        Inventory.currentCoins += 1000;
    }
#endif
}
