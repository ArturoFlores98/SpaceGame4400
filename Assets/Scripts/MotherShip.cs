using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public int scoreValue;
    private const float maxleft = -9f;
    private float speed = 5;
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if(transform.position.x <= maxleft)
            Destroy(gameObject);        
    }

    private void OnCollision2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIController.UpdateScore(scoreValue);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
