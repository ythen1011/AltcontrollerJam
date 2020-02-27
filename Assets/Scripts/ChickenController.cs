using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float skidDistance = 1f;
    [SerializeField] float onTargetTolerance = 0.2f;

    //bool jumping;


    [SerializeField] Vector3 target;
    [SerializeField] Vector3 targetDirection;
    Rigidbody rb;

    private bool onGround = true;
    private bool jumping = false;
    [Range(1, 10)]
    [SerializeField] float jumpVelocity;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
        targetDirection = (target - transform.position);

        if (targetDirection.magnitude > skidDistance)
        {
            targetDirection.Normalize();
            transform.LookAt(target);
        }
        else if(targetDirection.magnitude < skidDistance && targetDirection.magnitude > onTargetTolerance+0.1)
        {
            transform.LookAt(target);

        } else if(targetDirection.magnitude < onTargetTolerance)
        {
            transform.LookAt(Camera.main.transform.position);
        }

        // over a key
        RaycastHit[] hitObjects;
        hitObjects = Physics.RaycastAll(transform.position + new Vector3(0, 1, 0), Vector3.down);
        if (hitObjects.Length != 0)
        {
            foreach(RaycastHit hit in hitObjects)
            {

                if (hit.collider.gameObject.tag == "Key")
                {
                    if(hit.collider.gameObject.GetComponent<KeyControl>().justPressed == true)
                    {
                        if (onGround)
                        {
                            Jump();
                        }
                        jumping = true;

                    }
                    else
                    {
                        jumping = false;
                    }
                }
            }

        }
        
        // on a key
        RaycastHit[] hitObjects2;
        hitObjects2 = Physics.RaycastAll(transform.position + new Vector3(0, 0, 0), Vector3.down,0.1f);
        if (hitObjects.Length != 0)
        {
            foreach(RaycastHit hit in hitObjects2)
            {

                if (hit.collider.gameObject.tag == "Key")
                {
                    onGround = true;
                }
            }

        }

        //if (other.tag == "Key")
        //{
        //    if (other.GetComponent<KeyControl>().justPressed == true)
        //    {
        //        Jump();
        //    }
        //}


        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }


    private void FixedUpdate()
    {
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        rb.MovePosition(rb.position + targetDirection * speed * Time.fixedDeltaTime);

    }

    public void SetTarget(Vector3 pos)
    {
        target = pos;
       // target.y = 0;

    }

    public void Jump()
    {
        Debug.Log(rb.velocity.y);
        {
            if(rb.velocity.y < 1)
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            onGround = false;
        }
    }

   


}
