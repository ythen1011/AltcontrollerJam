using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player movement 

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject playerLeft, playerRight;
    // public int health = 4; 
    private Vector3 originalPosition;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        //CheckCollisons();
    }

    private void UpdatePosition()
    {
        Vector3 pos = transform.localPosition;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (pos.x > -20)
            {
                pos += Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (pos.x < 20)
            {
                pos += Vector3.right;
            }
        }
        transform.localPosition = pos;
    }
}

    //private void CheckCollisons()
  //  {
       // bool isSafe = true;

       // GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("CollidableObject");
        //foreach (GameObject go in gameObjects)
      //  {
           // CollidableObject collidableObject = go.GetComponent<CollidableObject>();

          //  if (collidableObject.isColliding(this.gameObject))
      //      {
             //   if (collidableObject.isSafe)
       //         {
              //      isSafe = true;

               //     if (collidableObject.isFox)
         //           {
                 //       isSafe = false;
            //            {
                   //         PlayerDied();
             //           }

           //         }
        //        } 
        //    }
     //   }
   // }
   // void PlayerDied()
 //   {
   //     ResetPosition();
   // }
  //  void ResetPosition()
 //   {
    //    transform.localPosition = originalPosition;

  //  }
//}
