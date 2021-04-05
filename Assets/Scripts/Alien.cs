using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;
    public GameObject explosion;
    
    public void Kill()
    {
        UIController.UpdateScore(scoreValue);

        AlienParent.allAliens.Remove(gameObject);

        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
