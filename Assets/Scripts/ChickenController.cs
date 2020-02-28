using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [Range(1, 10)] [SerializeField] float speed;

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

    bool overFox = false;

    public chickenState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = chickenState.runningToChord;
    }

    // Update is called once per frame
    void Update()
    {
      
        targetDirection = (target - transform.position);

        if (targetDirection.magnitude > skidDistance)
        {
            targetDirection.Normalize();
            transform.LookAt(target);
            state = chickenState.runningToChord;

        }
        else if(targetDirection.magnitude < skidDistance && targetDirection.magnitude > onTargetTolerance+0.1)
        {
            transform.LookAt(target);
            state = chickenState.inLocation;

        } else if(targetDirection.magnitude < onTargetTolerance)
        {
            transform.LookAt(Camera.main.transform.position);
            state = chickenState.inLocation;

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
                if(hit.collider.tag == "Fox")
                {
                    if (onGround == false)
                    {
                        overFox = true;
                    }
                }
            }

        }
        
        // on a key
        RaycastHit[] hitObjects2;
        hitObjects2 = Physics.RaycastAll(transform.position + new Vector3(0, 0, 0), Vector3.down,0.1f);
        if (hitObjects2.Length != 0)
        {
            foreach(RaycastHit hit in hitObjects2)
            {

                if (hit.collider.gameObject.tag == "Key")
                {
                    onGround = true;
                    if (overFox)
                    {
                        overFox = false;
                        GameManager.Instance.JumpedOverFox();
                    }
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


        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // fix rotation to only y
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
        {
            if(rb.velocity.y < 1)
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            onGround = false;
        }
    }

   
    public enum chickenState
    {
        runningToChord,
        inLocation,
        toDelete,
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fox")
        {
            state = chickenState.toDelete;
        }
    }

}
