using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordSender : MonoBehaviour
{
    [Range(0.1f,10f)] [SerializeField] float timeBetweenChords;
    [Range(0.1f,10f)] [SerializeField] float timeBetweenProgressions;

    [SerializeField] Keys key;

    [SerializeField] Chords chordController ;

    State state = State.initial;

    Progression currentProgression = Progression.None;

    [SerializeField] List<Chord> chordQueueVeiw = new List<Chord>();
    
    Queue<Chord> chordQueue = new Queue<Chord>(); // queues cannot be veiwed in editor


    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)Time.time*10000); // steed random

        currentProgression = (Progression)Random.Range(1, (int)Progression.count - 1); // get random progression
        
       

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.initial:
                state = State.generatingNextProgression;
                break;
            case State.waitingForNextProgression:
                break;
            case State.generatingNextProgression:
                GenerateNextProgression();
                state = State.sendingChord;
                break;
            case State.waitingForNextChord:
                break;
            case State.sendingChord:
                break;
            default:
                break;
        }




        chordQueueVeiw = new List<Chord>(chordQueue);
    }


    private void GenerateNextProgression()
    {
        switch (currentProgression)
        {
            case Progression.None:
                Debug.LogError("Error in progression assignment");
                break;
            case Progression.TonicDominantTonic:
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.tonic));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.dominant));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.tonic));
                break;
            case Progression.TonicSubdominantTonic:
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.tonic));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.subdominant));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.tonic));
                break;
            case Progression.TonicDominantSubdominantTonic:
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.tonic));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.dominant));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.subdominant));
                chordQueue.Enqueue(chordController.GetChordOfType(key, ChordFunction.tonic));
                break;
            case Progression.count:
                Debug.LogError("Error in progression assignment");
                break;
            default:
                Debug.LogError("Error in progression assignment");
                break;
        }
    }

    enum State
    {
        initial = 0,
        waitingForNextProgression,
        generatingNextProgression,
        waitingForNextChord,
        sendingChord,

    }

    enum Progression
    {
        None = 0,
        TonicDominantTonic,
        TonicSubdominantTonic,
        TonicDominantSubdominantTonic,
        
        count // used by random
    }
}
