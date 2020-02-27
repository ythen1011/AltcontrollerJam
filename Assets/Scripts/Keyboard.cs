using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MidiJack;

public class Keyboard : MonoBehaviour
{
    public const int numberOfKeys = 32;

    public Dictionary<KeyIndex, Note> KeymappingKeyToNote = new Dictionary<KeyIndex, Note>();
    public Dictionary<Note,KeyIndex> KeymappingNoteToKey = new Dictionary<Note,KeyIndex>();
    public Dictionary<KeyIndex, GameObject> keys = new Dictionary<KeyIndex, GameObject>();
    List<KeyIndex> indices = new List<KeyIndex>();
    List<Note> notes = new List<Note>();
    [SerializeField] List<GameObject> keyObjects = new List<GameObject>();
    List<Material> upMat = new List<Material>();
    [SerializeField] Material redMat;
    const int transpose = -12;

   // InputDevice piano;
   
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < numberOfKeys; i++)
        {
            keyObjects.Add(GameObject.Find("Key" + i));
            upMat.Add( keyObjects[i].GetComponent<MeshRenderer>().material);
        }

        System.Array indicesArray = KeyIndex.GetValues(typeof(KeyIndex));
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

        int length = indicesArray.Length < notesArray.Length ? indicesArray.Length : notesArray.Length;
        for(int i = 0; i < length; i++)
        {
            KeymappingKeyToNote[indices[i]] = notes[i];
            KeymappingNoteToKey[notes[i]] = indices[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {

       

        foreach (KeyIndex i in KeymappingKeyToNote.Keys)
        {
            AudioSource audio = keyObjects[(int)i].GetComponent<AudioSource>();
            float strenght = MidiMaster.GetKey(MidiChannel.All, (int)KeymappingKeyToNote[i]);
            if (MidiMaster.GetKeyDown(MidiChannel.All, (int)KeymappingKeyToNote[i]))
            {
                audio.volume = strenght;
                audio.pitch = Mathf.Pow(2, ((int)i + transpose) / 12f); // pitch shift for the key
                audio.time = 0;
                audio.Play();
            }

            if (audio.isPlaying&& MidiMaster.GetKey(MidiChannel.All, (int)KeymappingKeyToNote[i]) != 0)
            {
                if (audio.isPlaying)
                {
                    keyObjects[(int)i].GetComponent<MeshRenderer>().material = redMat;
                    
                }
            }
            else
            {
                keyObjects[(int)i].GetComponent<MeshRenderer>().material = upMat[(int)i];
                
              

            }
            if (audio.isPlaying && MidiMaster.GetKeyUp(MidiChannel.All, (int)KeymappingKeyToNote[i]))
            {
                audio.time = audio.clip.length * 0.8f;
            }

        }
       
        
    }

    public void StopSoftly(AudioSource audio)
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
    //gs3 = 8  + 24,
    //ab4 = 8  + 24,
    // a4 = 9  + 24,
    //as4 = 10 + 24,
    //bb4 = 10 + 24,
    // b4 = 11 + 24,


    // c4 = 0  + 36,
    //cs4 = 1  + 36,
    //db4 = 1  + 36,
    // d4 = 2  + 36,
    //ds4 = 3  + 36,
    //eb4 = 3  + 36,
    // e4 = 4  + 36,
    // f4 = 5  + 36,
    //fs4 = 6  + 36,
    //gb4 = 6  + 36,
    // g4 = 7  + 36,
    //gs4 = 8  + 36,
    //ab5 = 8  + 36,
    // a5 = 9  + 36,
    //as5 = 10 + 36,
    //bb5 = 10 + 36,
    // b5 = 11 + 36,

     
}


