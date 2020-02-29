using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MidiJack;
using System;

public class Keyboard : MonoBehaviour
{
    public const int numberOfKeys = 32;

    public Dictionary<KeyIndex, Note> KeymappingKeyToNote = new Dictionary<KeyIndex, Note>();
    public Dictionary<KeyIndex, Note> KeymappingComputerKeyboardToNote = new Dictionary<KeyIndex, Note>();
    public Dictionary<Note,KeyIndex> KeymappingNoteToKey = new Dictionary<Note,KeyIndex>();
    public Dictionary<KeyIndex, GameObject> keys = new Dictionary<KeyIndex, GameObject>();
    List<KeyIndex> indices = new List<KeyIndex>();
    List<Note> notes = new List<Note>();
    [SerializeField] List<GameObject> keyObjects = new List<GameObject>();
    List<Material> upMat = new List<Material>();
    public Material redMat;
    public const int transpose = -12;


   // InputDevice piano;
   
    // Start is called before the first frame update
    void Start()
    {
        // get the keys
        for(int i = 0; i < numberOfKeys; i++)
        {
            GameObject key = GameObject.Find("Key" + i);
            Debug.Assert(key != null);
            keyObjects.Add(key);
            keyObjects[i].GetComponent<KeyControl>().note = (KeyIndex) i;
        }

        // match up 0 indexes keys with the midi note value (there is actually no reason to do this it's an artifact from before i knew how sound would be implemented

        System.Array indicesArray = KeyIndex.GetValues(typeof(KeyIndex)); // ugly way of getting an itteratable from an enum
        foreach(KeyIndex i in indicesArray)
        {
            indices.Add(i);
            keys[i] = keyObjects[(int)i];
        }

        System.Array notesArray = Note.GetValues(typeof(Note));
        foreach (Note n in notesArray)
        {
            notes.Add(n);
        }

        int length = indicesArray.Length < notesArray.Length ? indicesArray.Length : notesArray.Length; // use the smaller one just to be safe
        for(int i = 0; i < length; i++)
        {
            KeymappingKeyToNote[indices[i]] = notes[i];
            KeymappingNoteToKey[notes[i]] = indices[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void StopSoftly(AudioSource audio) // unused
    {
        StartCoroutine("FadeVolume", audio);
    }

    IEnumerator FadeVolume(AudioSource audio)
    {
        for (float vol = audio.volume; vol >= 0; vol -= 0.1f)
        {
            audio.volume = vol;
            yield return null;
        }
        audio.Stop();
    }
}

public enum KeyIndex
{
   
     c1 = 0 ,
    cs1 = 1 ,
    db1 = 1 ,
     d1 = 2 ,
    ds1 = 3 ,
    eb1 = 3 ,
     e1 = 4 ,
     f1 = 5 ,
    fs1 = 6 ,
    gb1 = 6 ,
     g1 = 7 ,
    gs1 = 8 ,
    ab2 = 8 ,
     a2 = 9 ,
    as2 = 10,
    bb2 = 10,
     b2 = 11,

     c2 = 0  + 12,
    cs2 = 1  + 12,
    db2 = 1  + 12,
     d2 = 2  + 12,
    ds2 = 3  + 12,
    eb2 = 3  + 12,
     e2 = 4  + 12,
     f2 = 5  + 12,
    fs2 = 6  + 12,
    gb2 = 6  + 12,
     g2 = 7  + 12,
    gs2 = 8  + 12,
    ab3 = 8  + 12,
     a3 = 9  + 12,
    as3 = 10 + 12,
    bb3 = 10 + 12,
     b3 = 11 + 12,

     c3 = 0  + 24,
    cs3 = 1  + 24,
    db3 = 1  + 24,
     d3 = 2  + 24,
    ds3 = 3  + 24,
    eb3 = 3  + 24,
     e3 = 4  + 24,
     f3 = 5  + 24,
    fs3 = 6  + 24,
    gb3 = 6  + 24,
     g3 = 7  + 24,
         
}
public enum ComputerKeyboardKeyIndex
{
   
     c1 = KeyCode.Tab ,
    cs1 = KeyCode.Alpha1,
    db1 = KeyCode.Alpha1,
     d1 = KeyCode.Q ,
    ds1 = KeyCode.Alpha2,
    eb1 = KeyCode.Alpha2,
     e1 = KeyCode.W,
     f1 = KeyCode.Alpha3,
    fs1 = KeyCode.Alpha3,
    gb1 = KeyCode.E,
     g1 = KeyCode.R,
    gs1 = KeyCode.Alpha5,
    ab2 = KeyCode.Alpha5,
     a2 = KeyCode.T,
    as2 = KeyCode.Alpha6,
    bb2 = KeyCode.Alpha6,
     b2 = KeyCode.Y,
   
     c2 = KeyCode.U ,
    cs2 = KeyCode.Alpha8,
    db2 = KeyCode.Alpha8,
     d2 = KeyCode.W,
    ds2 = KeyCode.Alpha3,
    eb2 = KeyCode.Alpha3,
     e2 = KeyCode.E,
     f2 = KeyCode.R,
    fs2 = KeyCode.Alpha5,
    gb2 = KeyCode.Alpha5,
     g2 = KeyCode.T,
    gs2 = KeyCode.Alpha6,
    ab3 = KeyCode.Alpha6,
     a3 = KeyCode.Y,
    as3 = KeyCode.Alpha7,
    bb3 = KeyCode.Alpha7,
     b3 = KeyCode.U,

    c3 = 0  + 24,
    cs3 = 1  + 24,
    db3 = 1  + 24,
     d3 = 2  + 24,
    ds3 = 3  + 24,
    eb3 = 3  + 24,
     e3 = 4  + 24,
     f3 = 5  + 24,
    fs3 = 6  + 24,
    gb3 = 6  + 24,
     g3 = 7  + 24,
         
}


