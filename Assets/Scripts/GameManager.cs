using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//using UnityEngine.TextCore;
//public class ScoreComparitor : IComparer<Score>
//{
//    public int Compare(Score x, Score y)
//    {

//        //key value pair is non nullable

//        if(x.score > y.score)
//        {
//            return 1;
//        }

//        else if(x.score == y.score)
//        {
//            return 0;
//        } 

//        else if(x.score == y.score)
//        {
//            return -1;
//        }
//        else
//        {
//            return 0;
//        }
//    }
//}

[System.Serializable]
public class ScoreClass : IComparable<ScoreClass>
{
    public string name; 
    public int deaths;
    public int roundsSurvived;

    public ScoreClass(String _name, int _deaths, int _roundsSurvived)
    {
        name = _name;
        deaths = _deaths;
        roundsSurvived = _roundsSurvived;
    }
    public int CompareTo(ScoreClass other)
    {
        if (this.roundsSurvived > other.roundsSurvived)
        {
            return -1;
        }

        else if (this.roundsSurvived == other.roundsSurvived)
        {
            return 0;
        }

        else if (this.roundsSurvived == other.roundsSurvived)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
       // DontDestroyOnLoad(gameObject);
    }


    [SerializeField] GameObject popupPrefab;
    GameObject popup;

    [HideInInspector] public int round = 0;
    public int deaths = 0;

    //[SerializeField] List<ScoreStruct> scoreVeiw = new List<ScoreStruct>();

    //public int score = 0;
    public GameState gameState;

    float waitBeginTime;

    //[SerializeField] ScoreComparitor scoreComparator;

    //[SerializeField] int numberOfRounds = 10;
    [SerializeField] int initialnumberOfLives = 30;
    [SerializeField] bool waiting = false;

    public int lives;

    ChordSender chordSender;
    FoxManager foxManager;

    float readyForFoxesTime;

    AudioSource chickenDying;

    [SerializeField] GameObject feather;

    List<GameObject> feathers = new List<GameObject>();
    ScreenShake screenShake;
    float chickenWaitTime;
    float foxRunTime;
    float maximumChickenWaitTime = 15;
    float maximumFoxRunTime = 10;
    float foxWaitTime;
    [Range(0, 10)] [SerializeField] float defaultFoxWaitTime;

    [Range(0.1f,20)][SerializeField] float defaultFoxSpeed;
    //[Range(0.1f, 20)] [SerializeField] float foxSpeedAdjustmentAmount;
    [Range(0f, 10)] [SerializeField] float CMajorDifficultyAdjustment;

    [SerializeField] List<ScoreClass> scores = new List<ScoreClass>(); 

    [SerializeField] string playerName;

    private void OnApplicationQuit()
    {
        SaveScores();
        Destroy(this);
    }

    private void SaveScores()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString("scores_name_" + i, scores[i].name);
            PlayerPrefs.SetInt("scores_deaths_" + i, scores[i].deaths);
            PlayerPrefs.SetInt("scores_round_" + i, scores[i].roundsSurvived);
          //  ScoreVeiw s = new ScoreVeiw();
            //s.name = scores[i].Key;
            //s.score = scores[i].Value;
            //scoreVeiw.Add(s);
        }
        PlayerPrefs.Save();
    }

    Vector3 camPosition;
    Quaternion camRotation;

    //public void GameOver()
    //{

    //}

    //public void JumpedOverFox()
    //{

    //}



 




    // Start is called before the first frame update
    void Start()
    {
        chordSender = GetComponentInChildren<ChordSender>();
        foxManager = GetComponent<FoxManager>();
        chickenDying = GetComponent<AudioSource>();
        camPosition = Camera.main.transform.position;
        camRotation = Camera.main.transform.rotation;
        screenShake = Camera.main.GetComponent<ScreenShake>();
        foxManager.SetFoxSpeed(defaultFoxSpeed);
        foxWaitTime = defaultFoxWaitTime;
        lives = initialnumberOfLives;
        GetScores();
        gameState = GameState.begin;

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
                    deaths = PlayerPrefs.GetInt("scores_deaths_" + loadNumber, -1);
                }
                else
                {
                    StillToLoad = false;
                    break;
                }

                int roundsSurvived;
                if (PlayerPrefs.HasKey("scores_round_" + loadNumber))
                {
                    roundsSurvived = PlayerPrefs.GetInt("scores_round_" + loadNumber, -1);
                }
                else
                {
                    StillToLoad = false;
                    break;
                }


                scores.Add(new ScoreClass(name, deaths,roundsSurvived ));
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
        round = chordSender.chordNumber;
        switch (gameState)
        {
            case GameState.begin:
                gameState = GameState.newChord;
                break;
            case GameState.newChord:
                NewChord();
                break;
            case GameState.chickensRunningToLocation:
                CheckIfChickensAreThereYet();
                break;
            case GameState.readyForFoxes:
                WaitForFoxes();
                break;
            case GameState.sendFoxes:
                SendFoxes();
                break;
            case GameState.foxesChasing:
                CheckIfChickensAreDead();
                CheckIfFoxesAreDead();
                break;
            case GameState.gameEnding:
                CheckIfChickensAreDead();
                BringUpNameEnterPopup();
                break;
            case GameState.waitingForNameEnter:
                
                break;
            case GameState.ended:
                SceneManager.LoadScene("HighScores");
                gameState = GameState.outOfScene;
                break;
            case GameState.outOfScene:
                break;
            default:
                Debug.LogError("Uhandled Gamestate: " + gameState.ToString());
                break;
        }
        CleanUpFeathers();

        Camera.main.transform.position = camPosition + screenShake.GetShakePosition();
        Camera.main.transform.rotation = camRotation * screenShake.GetShakeRotation();


        // adjust the fox speed based on player's performance
        AdjustFoxWait();

    }

    private void BringUpNameEnterPopup()
    {
        popup = Instantiate(popupPrefab);
        popup.transform.SetParent(GameObject.Find("MainCanvas").transform,false);
        gameState = GameState.waitingForNameEnter;

    }

    public void UpdateScores()
    {
        playerName = popup.GetComponentInChildren<TMPro.TMP_InputField>().text;
        scores.Add(new ScoreClass(playerName, deaths, chordSender.sequenceNumber-1));
        scores.Sort();
        SaveScores();
        gameState = GameState.ended;
    }

    private void WaitForFoxes()
    {
        if(Time.time > readyForFoxesTime + foxWaitTime)
        {
            gameState = GameState.sendFoxes;
        }
    }

    private void AdjustFoxWait()
    {
        float time = defaultFoxWaitTime;

        //float deathsPerRound = (deaths + 10) / (round + 10 + 1); // round zero indexd

        //float dificultyAdjustment = (1 - deathsPerRound);

        //dificultyAdjustment = Mathf.Clamp(dificultyAdjustment, -1, 1);

        float cmajorAdjustment = chordSender.currentKey == MusicalKey.CMajor ? CMajorDifficultyAdjustment : 0f;

        time = defaultFoxWaitTime + cmajorAdjustment;// + dificultyAdjustment * foxSpeedAdjustmentAmount;

        foxWaitTime = Mathf.Clamp(foxWaitTime, 0, 5);

    }

    private void CleanUpFeathers()
    {
        feathers.RemoveAll(f => f == null);
        foreach (GameObject f in feathers)
        {
            if (!f.GetComponent<ParticleSystem>().isPlaying)
            {
                Destroy(f);
            }
        }
        feathers.RemoveAll(f => f == null);
    }

    private void CheckIfChickensAreDead(bool recursive = false)
    {
        List<GameObject> toDestroy = new List<GameObject>();
        toDestroy.Clear();
        chordSender.chickens.RemoveAll(chick => chick == null);
        foreach (ChickenController chick in chordSender.chickens)
        {
            if(lives <= 0 && !recursive)
            {
                chick.state = ChickenController.chickenState.toDelete;
                CheckIfChickensAreDead(true);
                gameState = GameState.gameEnding;
            }
            if (chick.state == ChickenController.chickenState.toDelete)
            {
                toDestroy.Add(chick.gameObject);
                chickenDying.Play();
                chickenDying.time = 1.08f;
                GameObject f = Instantiate(feather, chick.gameObject.transform.position, Quaternion.identity);
                feathers.Add(f);
                f.GetComponent<ParticleSystem>().Play();
                Camera.main.GetComponent<ScreenShake>().AddTrauma();
                deaths++;
                lives --;
                chordSender.scoreText.text = "Round: " + chordSender.sequenceNumber + "\nLives: " + Mathf.Clamp(lives,0,lives);
            }
        }
        foreach(GameObject del in toDestroy)
        {
            Destroy(del);
        }
        chordSender.chickens.RemoveAll(chick => chick == null );
    }

    private void CheckIfFoxesAreDead()
    {
        if (foxManager.GotAnyFoxes(foxRunTime,maximumFoxRunTime))
        {
            return;
        }
        else
        {
            gameState = GameState.newChord;
        }
    }

    private void SendFoxes()
    {
        foxManager.GenerateFoxes(chordSender.chickens);
        foxRunTime = Time.time;
        gameState = GameState.foxesChasing;
    }

    private void CheckIfChickensAreThereYet()
    {
        bool chickensReady = true;
        foreach(ChickenController chick in chordSender.chickens)
        {
            if (Time.time > chickenWaitTime + maximumChickenWaitTime) {
                chickensReady = true; // override
                break;
            }

            if(chick == null) // just to be safe
            {
                continue;
            }

            if (chick.state == ChickenController.chickenState.runningToChord) // still waiting on a chicken
            {
                chickensReady = false;
                break;
            }
           
        }
        if (chickensReady)
        {
            // all chickens there
            readyForFoxesTime = Time.time;
            gameState = GameState.readyForFoxes;
        }

    }

    // get the next chord, if it's the end of a sequence then wait a moment
    private void NewChord()
    {
        //if(chordSender.sequenceNumber > numberOfRounds)
        //{
        //    gameState = GameState.gameEnding;
        //    return;
        //}
        if (waiting)
        {
            if(waitBeginTime + chordSender.timeBetweenProgressions < Time.time)
            {
                return;
            }
            waiting = false;
        }

        bool chordGenerated = chordSender.NewChord(); // returns true if has chords in queue and just popped one, false if generated new sequence

        if (chordGenerated == false) // end of progeression wait a moment
        {
            waitBeginTime = Time.time;
        }
        else
        {
            chickenWaitTime = Time.time;
            gameState = GameState.chickensRunningToLocation;
        }
    }

    public enum GameState
    {
        begin,
       
        newChord,
        chickensRunningToLocation,
        readyForFoxes,
        sendFoxes,
        foxesChasing,
        gameEnding,
        waitingForNameEnter,
        ended,
        outOfScene,
    }

}
