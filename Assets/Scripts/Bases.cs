using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bases : MonoBehaviour
{
    public Sprite[] states;
    private SpriteRenderer sr;
    private int health;
    void Start()
    {
        health = 3;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            if(health <= 0)
                Destroy(gameObject);
            else
                sr.sprite = states[health - 1];
        }

        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
