using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI scoreText;


    public string number;
    public string name;
    public string score;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        numberText.text = number;
        nameText.text = name;
        scoreText.text = score;


    }
}
