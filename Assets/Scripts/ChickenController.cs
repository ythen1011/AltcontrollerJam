using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float skidDistance = 1f;
    [SerializeField] float onTargetTolerance = 0.2f;


    [SerializeField] GameObject cyl1;
    [SerializeField] GameObject cyl2;
    GameObject myCyl;

    [SerializeField] Vector3 target;
    [SerializeField] Vector3 targetDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCyl = cyl1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            myCyl = myCyl == cyl1 ? cyl2 : cyl1;
        }
        target = myCyl.transform.position;
        target.y = 0;
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
}
