using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    private int Direction = 1;

    public float Speed = 3.0f;
    public float MaxZ = 16.0f;
    public float MinZ = -16.0f;


    void Update()
    {
        transform.Translate(0, 0, Direction * Speed * Time.deltaTime);
        bool bounced = false;
        if (transform.position.z > MaxZ || transform.position.z < MinZ)
        {
            Direction = -Direction;
            bounced = true;
        }
        
        if (bounced)
            transform.Translate(0, 0, Direction * Speed * Time.deltaTime);
    }
}
