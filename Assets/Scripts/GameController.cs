using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] allAlienSets;
    private GameObject currentSet;
    private Vector2 spawnPosition = new Vector2(0, 10);
    private static GameController instance;

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
    }

    private IEnumerator SpawnWave()
    {
        AlienParent.allAliens.Clear();

        if (currentSet != null)
            Destroy(currentSet);

        yield return new WaitForSeconds(3);

        currentSet = Instantiate(allAlienSets[Random.Range(0, allAlienSets.Length)], spawnPosition, Quaternion.identity);
        UIController.UpdateWave();
    }
}
