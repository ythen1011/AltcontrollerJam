using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChordSender : MonoBehaviour
{




   // [Range(0.1f,10f)] [SerializeField] float timeBetweenChords;
    [Range(0.1f,10f)] [SerializeField] public float timeBetweenProgressions;

    [SerializeField] public MusicalKey currentKey;

    [SerializeField] Chords chordController ;

    [SerializeField] float timeWhenKeyShouldHaveBeenPressed = 0;

   // State state = State.initial;

    Progression currentProgression = Progression.None;

    [SerializeField] List<Chord> chordQueueVeiw = new List<Chord>();
    
    Queue<Chord> chordQueue = new Queue<Chord>(); // queues cannot be veiwed in editor

    [SerializeField] GameObject chickenObject;
    [SerializeField] public List<ChickenController> chickens = new List<ChickenController>();

    [SerializeField] GameObject[] spawnPoint = new GameObject[3];
    [SerializeField] Vector3 keyPositionOffset;

    [SerializeField]  TextMeshProUGUI chordText;
    [SerializeField]  TextMeshProUGUI keyText;
    [SerializeField]  TextMeshProUGUI functionText;

    [SerializeField] Keyboard piano;

    public int chordNumber = 0;

    public int difficulty = 3; // 3 is normal, 4 is hard


    // Start is called before the first frame update
    void Start()
    {


        currentProgression = (Progression)Random.Range(1, (int)Progression.count); // get random progression
        currentKey = MusicalKey.CMajor;


    }


    public void AssignPositions(Chord chord)
    {
        chordText.text = chord.chord.ToString().Substring(0, chord.chord.ToString().Length-1);
        keyText.text = "Key: " + chord.keyThisChordIsIn.ToString();
        Debug.Log(chord.chord.ToString());
        while (chickens.Count < chord.notes.Count)
        {
            GameObject chicken = Instantiate(chickenObject, spawnPoint[chickens.Count % spawnPoint.Length].transform.position+ new Vector3(0,keyPositionOffset.y,0), Quaternion.identity);
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
            GameManager.Instance.GameOver();
        }
        

        chordQueueVeiw = new List<Chord>(chordQueue);
    }

    public bool NewChord()
    {
        if (chordQueue.Count == 0)
        {
            GenerateNextProgression();
            return false;
        }
        else
        {
            Chord chord = chordQueue.Dequeue();
            timeWhenKeyShouldHaveBeenPressed = Time.time;

            AssignPositions(chord);
            return true;
        }
    }

    

    private void GenerateNextProgression()
    {
        if(chordNumber/(int)12 %2 != 0)
        {
            currentKey = (MusicalKey)Random.Range(1, (int)MusicalKey.count); // maybe switch key
        }

        currentProgression = (Progression)Random.Range(1, (int)Progression.count); // get random progression
        switch (currentProgression)
        {
            case Progression.None:
                Debug.LogError("Error in progression assignment");
                break;
            case Progression.TonicDominantTonic:
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.tonic));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.dominant));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.tonic));
                chordNumber += 3;
                break;
            case Progression.TonicSubdominantTonic:
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.tonic));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.subdominant));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.tonic));
                chordNumber += 3;
                break;
            case Progression.TonicDominantSubdominantTonic:
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.tonic));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.dominant));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.subdominant));
                chordQueue.Enqueue(chordController.GetChordOfType(currentKey, ChordFunction.tonic));
                chordNumber += 4;
                break;
            case Progression.count:
                Debug.LogError("Error in progression assignment");
                break;
            default:
                Debug.LogError("Error in progression assignment");
                break;
        }
    }

    //enum State
    //{
    //    initial = 0,
    //    waitingForNextProgression,
    //    generatingNextProgression,
    //    waitingForNextChord,
    //    foxesRunning,
    //    chickensRunning,

    //}

    enum Progression
    {
        None = 0,
        TonicDominantTonic,
        TonicSubdominantTonic,
        TonicDominantSubdominantTonic,
        
        count // used by random
    }
}
