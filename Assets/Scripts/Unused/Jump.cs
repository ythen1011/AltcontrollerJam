using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// jump movement 

public class Jump : MonoBehaviour
{
    private float jumpSpeed = 5;
    private Rigidbody rigidBody;
    private bool onGround = true;
    private const int MAX_JUMP = 1;
    private int currentJump = 0;
    [Range(1, 10)]
    public float jumpVelocity;

    // Start is called before the first frame update
    void Start()

    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && (onGround || MAX_JUMP > currentJump))
        {
            rigidBody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            onGround = false;
            currentJump++;            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        currentJump = 0;
    }
}
