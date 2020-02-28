using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{

    public GameObject targetChicken;
    public foxState state;

    [Range(1, 20)] [SerializeField] public float speed;

    [SerializeField] Vector3 targetDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        state = foxState.chaseChicken;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case foxState.waiting:
                break;
            case foxState.chaseChicken:
                ChaseChicken();
                if (OnPiano())
                {
                    state = foxState.runOffscreen;
                }
                break;
            case foxState.runOffscreen:
                RunOffscreen();
                if(transform.position.z < Camera.main.transform.position.z)
                {
                    state = foxState.toDelete;
                }
                break;
            case foxState.toDelete:
                break;
            default:
                break;
        }
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // fix rotation to only y

    }

    private bool OnPiano()
    {
        RaycastHit[] hitObjects;
        hitObjects = Physics.RaycastAll(transform.position+ new Vector3(0,1,0), Vector3.down);
        if (hitObjects.Length != 0)
        {
            foreach (RaycastHit hit in hitObjects)
            {
                if (hit.collider.gameObject.tag == "Key")
                {
                    return true;
                }
                    
            }

        }
        return false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + targetDirection * speed * Time.fixedDeltaTime);
    }

    public enum foxState
    {
        waiting,
        chaseChicken, // after spawn run towards chicken
        runOffscreen, // chickens avoided we can run offscreen now
        toDelete
    }


    private void ChaseChicken()
    {
        if(targetChicken == null)
        {
            state = foxState.toDelete;
            return;
        }
        targetDirection = (targetChicken.transform.position - transform.position);
        targetDirection.y = 0;
        targetDirection.Normalize();
        transform.LookAt(targetChicken.transform.position);
    }
    
    private void RunOffscreen()
    {

    }

}
