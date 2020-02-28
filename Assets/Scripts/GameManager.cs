using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int score = 0;
    public GameState gameState;

    float waitBeginTime;

    [SerializeField] bool waiting = false;

    ChordSender chordSender;
    FoxManager foxManager;




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
   
    }

    // Update is called once per frame
    void Update()
    {
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
