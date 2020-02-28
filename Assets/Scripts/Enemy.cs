using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// fox movement 

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform Fox;
    [SerializeField] private Transform respawnPoint;

    public int Score;
    public TextMeshProUGUI ScoreText;

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
                Score++;
                ScoreText.text = "Score: " + Score.ToString();
                pos = respawnPoint.transform.position;
               // GameManager.Instance.score++;       when used score in inspector +3 each time fox passes 
            }
            //if 
            
        }
        transform.localPosition = pos;
       

    }
}
