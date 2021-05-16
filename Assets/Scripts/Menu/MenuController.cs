using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject shopMenu;
    public GameObject inGameMenu;
    public GameObject pauseMenu;
    public AudioClip menuSFX;

    public static MenuController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ReturnToMainMenu();
    }

    public void OpenMain()
    {
        instance.mainMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
        AudioController.PlaySoundEffect(menuSFX);
    }

    public static void OpenGameOver()
    {
        instance.gameOverMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public void OpenShop()
    {
        instance.mainMenu.SetActive(false);
        instance.shopMenu.SetActive(true);
        AudioController.PlaySoundEffect(menuSFX);
    }

    public void CloseShop()
    {
        instance.mainMenu.SetActive(true);
        instance.shopMenu.SetActive(false);
        AudioController.PlaySoundEffect(menuSFX);
    }

    public void OpenInGame()
    {
        Time.timeScale = 1;

        Ship player = GameObject.FindGameObjectWithTag("Player").GetComponent<Ship>();
        player.shipStats.currentHealth = player.shipStats.maxHealth;
        player.shipStats.currentLives = player.shipStats.maxLives;

        //UIController.UpdateHealthbar(player.shipStats.currentHealth);
        

        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);

        GameController.SpawnNewWave();
    }

    public void OpenPause()
    {
        Time.timeScale = 0;

        instance.inGameMenu.SetActive(false);
        instance.pauseMenu.SetActive(true);
        AudioController.PlaySoundEffect(menuSFX);
    }

    public void ClosePause()
    {
        Time.timeScale = 1;

        instance.inGameMenu.SetActive(true);
        instance.pauseMenu.SetActive(false);
        AudioController.PlaySoundEffect(menuSFX);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;

        instance.gameOverMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(false);

        instance.mainMenu.SetActive(true);
        AudioController.PlaySoundEffect(menuSFX);

        GameController.CancelGame();
    }

}
