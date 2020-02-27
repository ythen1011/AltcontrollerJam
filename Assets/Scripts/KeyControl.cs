using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class KeyControl : MonoBehaviour
{

    public bool justPressed;
    public Material defaultMat;
    public Material redMat;
    MeshRenderer renderer;
    AudioSource audio;
    public KeyIndex myIndex;
    Keyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        keyboard = GetComponentInParent<Keyboard>();
        renderer = GetComponent<MeshRenderer>();
        redMat = keyboard.redMat;
    }

    // Update is called once per frame
    void Update()
    {
        float strenght = MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[myIndex]);
        if (MidiMaster.GetKeyDown(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[myIndex]))
        {
            audio.volume = strenght;
            audio.pitch = Mathf.Pow(2, ((int)myIndex + Keyboard.transpose) / 12f); // pitch shift for the key
            audio.time = 0;
            audio.Play();
        }

        if (audio.isPlaying && MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[myIndex]) != 0)
        {
            if (audio.isPlaying)
            {
                renderer.material = redMat;
                justPressed = true;

            }
        }
        else
        {
            renderer.material = defaultMat;
            justPressed = false;


        }
        if (audio.isPlaying && MidiMaster.GetKeyUp(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[myIndex]))
        {
            audio.time = audio.clip.length * 0.8f;
        }
    }
}
