using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class KeyControl : MonoBehaviour
{

    public bool justPressed;
    Material defaultMat;
    Material redMat;
    MeshRenderer renderer;
    AudioSource audio;
    public KeyIndex note;
    Keyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        keyboard = GetComponentInParent<Keyboard>();
        renderer = GetComponent<MeshRenderer>();
        defaultMat = renderer.material;
        redMat = keyboard.redMat;
    }

    // Update is called once per frame
    void Update()
    {
        float strenght = MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]);

        if (MidiMaster.GetKeyDown(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]))
        {
            audio.volume = strenght;
            audio.pitch = Mathf.Pow(2, ((int)note + Keyboard.transpose) / 12f); // pitch shift for the key
            audio.time = 0;
            justPressed = true;
            audio.Play();
        }

        if (audio.isPlaying && MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]) != 0)
        {
            if (audio.isPlaying)
            {
                renderer.material = redMat;

            }
        }
        else
        {
            renderer.material = defaultMat;
            justPressed = false;


        }
        if (audio.isPlaying && MidiMaster.GetKeyUp(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]))
        {
            audio.time = audio.clip.length * 0.8f;
        }
        HandleAlternativeComputerKeyboardInput();
    }


    private void HandleAlternativeComputerKeyboardInput()
    {
        float strenght = MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]);

        if (MidiMaster.GetKeyDown(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]))
        {
            audio.volume = strenght;
            audio.pitch = Mathf.Pow(2, ((int)note + Keyboard.transpose) / 12f); // pitch shift for the key
            audio.time = 0;
            justPressed = true;
            audio.Play();
        }

        if (audio.isPlaying && MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]) != 0)
        {
            if (audio.isPlaying)
            {
                renderer.material = redMat;

            }
        }
        else
        {
            renderer.material = defaultMat;
            justPressed = false;

        }
        if (audio.isPlaying && MidiMaster.GetKeyUp(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[note]))
        {
            audio.time = audio.clip.length * 0.8f;
        }
    }



}
