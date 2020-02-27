using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI ScoreText;
    public GameObject Fox;
    public GameObject Chicken;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Fox.transform.position;

        if (pos.z <= -8)
        {
            score++;
            ScoreText.text = "Score: " + score.ToString();
        }
       
    }
}
