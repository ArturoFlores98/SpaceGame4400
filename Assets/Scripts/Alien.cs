using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;
    public AudioClip dieSfx;
    public GameObject coinPF;
    public GameObject lifePF;
    public GameObject healthPF;
    public GameObject explosion;
    private const int lifeChance = 2;
    private const int healthChance = 15;
    private const int coinChance = 70;
    
    
    public void Kill()
    {
        UIController.UpdateScore(scoreValue);

        AlienParent.allAliens.Remove(gameObject);

        int random = Random.Range(0,1000);

        if(random <= lifeChance)
            Instantiate(lifePF, transform.position, Quaternion.identity);
        else if(random <= healthChance)
            Instantiate(healthPF, transform.position, Quaternion.identity);
        else if(random <= coinChance)
            Instantiate(coinPF, transform.position, Quaternion.identity);

        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioController.PlaySoundEffect(dieSfx);

        AudioController.UpdateBattleMusicDelay(AlienParent.allAliens.Count);

        if(AlienParent.allAliens.Count == 0)
            GameController.SpawnNewWave();

        Destroy(gameObject);
    }
}
