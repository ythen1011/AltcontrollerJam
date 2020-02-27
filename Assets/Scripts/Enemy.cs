using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// fox movement 

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform Fox;
    [SerializeField] private Transform respawnPoint;

    public float moveSpeed = 5f;
    public bool moveForward = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localPosition;

        if (moveForward)
        {
            pos.z += Vector3.forward.z * moveSpeed * Time.deltaTime;

            if (pos.z >= 10)
            {
                pos.z = -10;
            }
        }
        else
        {
            pos.z += Vector3.back.z * moveSpeed * Time.deltaTime;

            if (pos.z <= -10)
            {
                pos.z = 10;
            }
        }
        transform.localPosition = pos;
        Fox.transform.position = respawnPoint.transform.position;

    }
}
