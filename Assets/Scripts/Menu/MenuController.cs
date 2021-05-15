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
    }

    public void CloseShop()
    {
        instance.mainMenu.SetActive(true);
        instance.shopMenu.SetActive(false);
    }

    public void OpenInGame()
    {
        Time.timeScale = 1;

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
    }

    public void ClosePause()
    {
        Time.timeScale = 1;

        instance.inGameMenu.SetActive(true);
        instance.pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;

        instance.gameOverMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(false);

        instance.mainMenu.SetActive(true);

        GameController.CancelGame();
    }

    public static void CloseWindow(GameObject go)
    {
        go.SetActive(false);
    }
}
