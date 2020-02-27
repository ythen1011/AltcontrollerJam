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


        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }


    private void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + targetDirection * speed * Time.fixedDeltaTime);

        
    }

    public void SetTarget(Vector3 pos)
    {
        target = pos;
       // target.y = 0;

    }

    public void Jump()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Key")
        {
            if(other.GetComponent<KeyControl>().justPressed == true)
            {
                Jump();
            }
        }
    }

}
