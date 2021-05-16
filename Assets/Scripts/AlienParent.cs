using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienParent : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject mothershipPrefab;
    private Vector3 updownMove = new Vector3(.2f,0,0);
    private Vector3 leftrightMove = new Vector3(0,.2f,0);
    private Vector3 motherShipSpawnPos = new Vector3(9.84f, 6.5f, 0);

    private const float left = -12f;
    private const float right = 12f;
    private const float start_y = 2f;
    private const float maxmovespeed = 0.04f;
    private float enemyMove = 0.02f;
    private const float enemyMoveSpeed = 0.008f;

    private float shootTimer = 3f;
    private const float shootTime = 3f;

    private float mothershipTimer = 3f;
    private const float motherShipMin = 15f;
    private const float motherShipMax = 60f;
    private bool toRight;
    private bool entering = true;
    public static List<GameObject> allAliens = new List<GameObject>();
    public AudioClip shootSFX;
        void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))    
               allAliens.Add(go);
    }
    
    void Update()
    {
        if(entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);

            if(transform.position.y <= start_y)
                entering = false;
        }
        else
        {
             if(enemyMove <= 0)
            EnemiesMove();

        if(shootTimer <= 0)
            Shoot();
        
        if (mothershipTimer <= 0)
            SpawnMotherShip();

        enemyMove -= Time.deltaTime;
        shootTimer -= Time.deltaTime;
        mothershipTimer -= Time.deltaTime;
        }

    }

    private void EnemiesMove()
    {
        if(allAliens.Count > 0)
        {
            int amtBoundary = 0;

            for (int i = 0; i < allAliens.Count; i++)
            {
                if (toRight)
                    allAliens[i].transform.position += updownMove;
                else 
                     allAliens[i].transform.position -= updownMove;
                
                if (allAliens[i].transform.position.x > right ||allAliens[i].transform.position.x < left)
                        amtBoundary++;
            }

            if(amtBoundary > 0)
            {
                for (int i = 0; i <allAliens.Count; i++)
                     allAliens[i].transform.position -= leftrightMove;

                toRight = !toRight;
            }
            enemyMove = GetMoveSpeed();

        }   
    }

    private void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;

        Instantiate(bulletPrefab, pos, Quaternion.identity);
        AudioController.PlaySoundEffect(shootSFX);

        shootTimer = shootTime;
    }

    private void SpawnMotherShip()
    {
        Instantiate(mothershipPrefab, motherShipSpawnPos, Quaternion.identity);
        mothershipTimer = Random.Range(motherShipMin, motherShipMax);
    }

    private float GetMoveSpeed()
    {
        float f = allAliens.Count * enemyMoveSpeed;

        if (f < maxmovespeed)
            return maxmovespeed; 
        else 
            return f;
    }
}
