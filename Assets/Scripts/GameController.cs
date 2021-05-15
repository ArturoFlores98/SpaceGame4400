using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] allAlienSets;
    private GameObject currentSet;
    private Vector2 spawnPosition = new Vector2(0, 10);
    private static GameController instance;
    public AudioClip waveComplete;
    public AudioClip newWaveSFX;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static void SpawnNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    public static void CancelGame()
    {
        instance.StopAllCoroutines();

        AlienParent.allAliens.Clear();

        if (instance.currentSet != null)
            Destroy(instance.currentSet);

        UIController.ResetUI();
        AudioController.StopBattleMusic();
    }

    private IEnumerator SpawnWave()
    {
        AudioController.UpdateBattleMusicDelay(1);
        AudioController.StopBattleMusic();
        AlienParent.allAliens.Clear();

        if (currentSet != null)
            Destroy(currentSet);
        AudioController.PlaySoundEffect(waveComplete);

        yield return new WaitForSeconds(3);

        AudioController.PlaySoundEffect(newWaveSFX);
        currentSet = Instantiate(allAlienSets[Random.Range(0, allAlienSets.Length)], spawnPosition, Quaternion.identity);
        UIController.UpdateWave();
        AudioController.PlayBattleMusic();
    }
}
