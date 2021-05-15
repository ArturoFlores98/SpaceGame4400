using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public float fallspeed;
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallspeed);
    }

    public abstract void PickMeUp();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            PickMeUp();
    }
}
