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

    [SerializeField] GameObject chickenObject;
    [SerializeField] List<ChickenController> chickens = new List<ChickenController>();

    [SerializeField] GameObject[] spawnPoint = new GameObject[3];
    [SerializeField] Vector3 keyPositionOffset;


    [SerializeField] Keyboard piano;


    // Start is called before the first frame update
    void Start()
    {
        //Random.InitState((int)Time.time*10000); // steed random

        currentProgression = (Progression)Random.Range(1, (int)Progression.count - 1); // get random progression
        
       

    }


    public void NewChord(Chord chord)
    {
        //Debug.Logg(chord.chord);
        while (chickens.Count < chord.notes.Count)
        {
            GameObject chicken = Instantiate(chickenObject, spawnPoint[chickens.Count%3].transform.position, Quaternion.identity);
            chickens.Add(chicken.GetComponent<ChickenController>());
        }
       for(int i = 0; i < chord.notes.Count; i++)
        {
            Vector3 position = piano.keys[piano.KeymappingNoteToKey[chord.notes[i]]].transform.position + keyPositionOffset;
            chickens[i].SetTarget(position);   
        }

    }

    // Update is called once per frame
    void Update()
    {

        if(chickens.Count == 0)
        {
            // you loose
        }

        foreach(ChickenController chick in chickens)
        {
            if(chick == null)
            {
                chickens.Remove(chick);
            }
        }
        switch (state)
        {
            case State.initial:
                state = State.generatingNextProgression;
                break;

            case State.waitingForNextProgression:
                if (false) // chord still falling
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
                if (false) // chord still falling
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

                NewChord(chord);


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
