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

    private void Start()
    {
        SpawnNewWave();
    }

    public static void SpawnNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        if(currentSet != null)
            Destroy(currentSet);

        yield return new WaitForSeconds(3);

        currentSet = Instantiate(allAlienSets[Random.Range(0, allAlienSets.Length)], spawnPosition, Quaternion.identity);
        UIController.UpdateWave();
    }
}
