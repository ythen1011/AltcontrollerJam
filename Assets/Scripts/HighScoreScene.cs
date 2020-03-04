using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighScoreScene : MonoBehaviour
{
    List<ScoreStruct> scores = new List<ScoreStruct>();

    // Start is called before the first frame update
    void Start()
    {
        // instance highscore display
        GetScores();
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

                scores.Add(new ScoreStruct(name, deaths,roundsSurvived));
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
