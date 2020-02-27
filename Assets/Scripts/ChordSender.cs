using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordSender : MonoBehaviour
{




    [Range(0.1f,10f)] [SerializeField] float timeBetweenChords;
    [Range(0.1f,10f)] [SerializeField] float timeBetweenProgressions;

    [SerializeField] MusicalKey key;

    [SerializeField] Chords chordController ;

    [SerializeField] float timeWhenKeyShouldHaveBeenPressed = 0;

    State state = State.initial;

    Progression currentProgression = Progression.None;

    [SerializeField] List<Chord> chordQueueVeiw = new List<Chord>();
    
    Queue<Chord> chordQueue = new Queue<Chord>(); // queues cannot be veiwed in editor
    public GameObject ___fox___;
    public Keyboard piano;

    GameObject foxCheck = null;

    // Start is called before the first frame update
    void Start()
    {
        //Random.InitState((int)Time.time*10000); // steed random

        currentProgression = (Progression)Random.Range(1, (int)Progression.count - 1); // get random progression
        
       

    }


    public void createYellow(Chord chord)
    {
       Debug.LogWarning(chord.chord.ToString());
       foreach (Note n in chord.notes)
        {
            
            Vector3 position = piano.keys[piano.KeymappingNoteToKey[n]].transform.position + new Vector3(0, 4, -2);
            foxCheck = Instantiate(___fox___,position,Quaternion.identity);
            Debug.Log(n.ToString() + " "+ (int)piano.KeymappingNoteToKey[n]);
          
        }

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
                if (foxCheck != null) // chord still falling
                {
                    timeWhenKeyShouldHaveBeenPressed = Time.time;
                    break;
                }
                if (timeWhenKeyShouldHaveBeenPressed + timeBetweenProgressions > Time.time)
                {
                    break;
                }
                else
                {
                    state = State.generatingNextProgression;
                }
                break;

            case State.generatingNextProgression:
                GenerateNextProgression();
                state = State.sendingChord;
                break;

            case State.waitingForNextChord:
                if (foxCheck != null) // chord still falling
                {
                    timeWhenKeyShouldHaveBeenPressed = Time.time;
                    break;
                }

                if (timeWhenKeyShouldHaveBeenPressed + timeBetweenChords > Time.time) 
                {
                    break;
                }
                else
                {
                    state = State.sendingChord;
                }
                break;

            case State.sendingChord:
                Chord chord = chordQueue.Dequeue();
                timeWhenKeyShouldHaveBeenPressed = Time.time;

                createYellow(chord);


                if (chordQueue.Count > 0)
                {
                    
                    state = State.waitingForNextChord;
                }
                else
                {
                    state = State.waitingForNextProgression;
                }
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
