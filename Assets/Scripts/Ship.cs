using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public ShipStats shipStats; 
    public GameObject shot;
    private Vector2 offScreenPosition = new Vector2(0,-20f);
    private Vector2 startScreenPosition = new Vector2(0, -6.1f);
    private const float left = -6.82f;
    private const float right = 6.82f;
    private bool shooting;
    
    private void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;

        transform.position = startScreenPosition;
    }
    void Update()
    {
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > left)
                transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);


            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < right)
                transform.Translate(Vector2.right * Time.deltaTime *shipStats.shipSpeed);

            if (Input.GetKey(KeyCode.Space) && !shooting)
                StartCoroutine(Shoot());
    }

    private void TakeDamage()
    {
        shipStats.currentHealth--;

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;

            if (shipStats.currentLives <= 0)
            {
                Debug.Log("GameOver");
                //GameOver
            }
            else
            {
                StartCoroutine(Respawn());
            }
        }
    }
    
    private IEnumerator Shoot()
    {
        shooting = true;
        Instantiate(shot, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(shipStats.fireRate);
        shooting = false;
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPosition;
        yield return new WaitForSeconds(2);
        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startScreenPosition;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player");
            TakeDamage();
            Destroy(collision.gameObject);         
        }
    }
}


