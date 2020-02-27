using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

// private void CheckCollisons()
//{
    // bool isSafe = true;

   // GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Fox");
 //  {
      //  CollidableObject collidableObject = go.GetComponent<Fox>();

       // if (collidableObject.isColliding(this.gameObject))
       // {
        //    Score--;
        //    ScoreText.text = "Score: " + Score.ToString();
        
    


