using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject shot;

    private const float left = -7.85f;
    private const float right = 7.85f;

    private float speed = 3;
    private bool shooting;
    private float cools = 0.5f;
    void Update()
    {
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > left)
                transform.Translate(Vector2.left * Time.deltaTime *speed);


            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < right)
                transform.Translate(Vector2.right * Time.deltaTime *speed);

            if (Input.GetKey(KeyCode.Space) && !shooting)
                StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        Instantiate(shot, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cools);
        shooting = false;
    }
}
