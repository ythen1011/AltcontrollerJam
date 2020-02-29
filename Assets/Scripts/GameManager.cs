using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.TextCore;

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
    }


    public int round= 0;
    public int deaths = 0;

    public int score = 0;
    public GameState gameState;

    float waitBeginTime;

    [SerializeField] bool waiting = false;

    ChordSender chordSender;
    FoxManager foxManager;
  

    AudioSource chickenDying;

    [SerializeField] GameObject feather;

    [SerializeField] List<GameObject> feathers;
    ScreenShake screenShake;

    [SerializeField] float defaultFoxSpeed = 10f;
    [SerializeField] float foxSpeedAdjustmentAmount = 8f;
    float CMajorDifficultyAdjustment = 2f;

    Vector3 camPosition;
    Quaternion camRotation;

    public void GameOver()
    {

    }

    public void JumpedOverFox()
    {

    }








    // Start is called before the first frame update
    void Start()
    {
        chordSender = GetComponentInChildren<ChordSender>();
        foxManager = GetComponent<FoxManager>();
        chickenDying = GetComponent<AudioSource>();
        camPosition = Camera.main.transform.position;
        camRotation = Camera.main.transform.rotation;
        screenShake = Camera.main.GetComponent<ScreenShake>();
        
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
            case GameState.sendFoxes:
                SendFoxes();
                break;
            case GameState.foxesChasing:
                CheckIfChickensAreDead();
                CheckIfFoxesAreDead();
                break;
            default:
                break;
        }
        CleanUpFeathers();

        Camera.main.transform.position = camPosition + screenShake.GetShakePosition();
        Camera.main.transform.rotation = camRotation * screenShake.GetShakeRotation();


        // adjust the fox speed based on player's performance
        AdjustFoxSpeed();

    }

    private void AdjustFoxSpeed()
    {
        float foxSpeed = defaultFoxSpeed;

        float deathsPerRound = (deaths + 10) / (round + 10 + 1); // round zero indexd

        float dificultyAdjustment = (1 - deathsPerRound);

        dificultyAdjustment = Mathf.Clamp(dificultyAdjustment, -1, 1);

        float cmajorAdjustment = chordSender.currentKey == MusicalKey.CMajor ? CMajorDifficultyAdjustment : 0f;

        foxSpeed = defaultFoxSpeed + cmajorAdjustment + dificultyAdjustment * foxSpeedAdjustmentAmount;


        foxManager.SetFoxSpeed(foxSpeed);
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

    private void CheckIfChickensAreDead()
    {
        List<GameObject> toDestroy = new List<GameObject>();
        toDestroy.Clear();
        chordSender.chickens.RemoveAll(chick => chick == null);
        foreach (ChickenController chick in chordSender.chickens)
        {
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
        if (foxManager.GotAnyFoxes())
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
        gameState = GameState.foxesChasing;
    }

    private void CheckIfChickensAreThereYet()
    {
        foreach(ChickenController chick in chordSender.chickens)
        {
            if(chick == null) // just to be safe
            {
                continue;
            }

            if (chick.state == ChickenController.chickenState.runningToChord) // still waiting on a chicken
            {
                return;
            }
        }

        // all chickens there
        gameState = GameState.sendFoxes;

    }

    // get the next chord, if it's the end of a sequence then wait a moment
    private void NewChord()
    {
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
            gameState = GameState.chickensRunningToLocation;
        }
    }

    public enum GameState
    {
        begin,
       
        newChord,
        chickensRunningToLocation,
        sendFoxes,
        foxesChasing,


    }

}
