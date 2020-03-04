using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class HighScoreScene : MonoBehaviour
{
    List<ScoreClass> scores = new List<ScoreClass>();
    [SerializeField] GameObject content;
    [SerializeField] GameObject score;
    float height;

    // Start is called before the first frame update
    void Start()
    {
        GetScores();
        AddScores();
        height = score.GetComponent<RectTransform>().rect.height;
    }

    private void AddScores()
    {
       scores.Sort();
       for(int i = 0; i < scores.Count; i++)
        {
            GameObject s = Instantiate(score);
            s.transform.parent = content.transform;
            ScoreTextController text = s.GetComponent<ScoreTextController>();
            text.number = (i + 1).ToString();
            text.name = scores[i].name;
            text.score = scores[i].roundsSurvived.ToString();
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x,100 * (i + 1));
        }
    }

    private void GetScores()
    {
        scores.Clear();
        bool StillToLoad = true;
        int loadNumber = 0;
        while (StillToLoad)
        {
            try
            {
                string name;
                if (PlayerPrefs.HasKey("scores_name_" + loadNumber))
                {
                    name = PlayerPrefs.GetString("scores_name_" + loadNumber, "File Load Name Error");
                }
                else
                {
                    StillToLoad = false;
                    break;
                }

                int deaths;
                if (PlayerPrefs.HasKey("scores_deaths_" + loadNumber))
                {
                    deaths = PlayerPrefs.GetInt("scores_deaths_" + loadNumber ,- 1);
                }
                else
                {
                    StillToLoad = false;
                    break;
                }

                int roundsSurvived;
                if (PlayerPrefs.HasKey("scores_round_" + loadNumber))
                {
                    roundsSurvived = PlayerPrefs.GetInt("scores_round_" + loadNumber ,- 1);
                }
                else
                {
                    StillToLoad = false;
                    break;
                }

                scores.Add(new ScoreClass(name, deaths,roundsSurvived));
                loadNumber++;
            }
            catch (PlayerPrefsException)
            {
                StillToLoad = false;
                break;
                throw;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
