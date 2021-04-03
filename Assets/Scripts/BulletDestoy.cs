using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestoy : MonoBehaviour
{
    public float secs;
    void Start()
    {
        Destroy(gameObject, secs);
    }

    void Update()
    {
        
    }
}
