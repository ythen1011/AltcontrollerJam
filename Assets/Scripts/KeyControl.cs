using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class KeyControl : MonoBehaviour
{

    public bool justPressed;
    Material defaultMat;
    Material redMat;
    MeshRenderer meshRenderer;
    AudioSource audioSource;
    public Note note;
    public KeyIndex keyOnKeyboard;
    public ComputerKeyboardKeyIndex keyOnComputerKeyboard;
    Keyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        keyboard = GetComponentInParent<Keyboard>();
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMat = meshRenderer.material;
        redMat = keyboard.redMat;
    }

    // Update is called once per frame
    void Update()
    {
        float strenght = MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[keyOnKeyboard]);

        if (MidiMaster.GetKeyDown(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[keyOnKeyboard]))
        {
            audioSource.volume = strenght;
            audioSource.pitch = Mathf.Pow(2, ((int)keyOnKeyboard + Keyboard.transpose) / 12f); // pitch shift for the key
            audioSource.time = 0;
            justPressed = true;
            audioSource.Play();
        }

        if (audioSource.isPlaying && MidiMaster.GetKey(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[keyOnKeyboard]) != 0)
        {
            if (audioSource.isPlaying)
            {
                meshRenderer.material = redMat;

            }
        }
        else
        {
            meshRenderer.material = defaultMat;
            justPressed = false;


        }
        if (audioSource.isPlaying && MidiMaster.GetKeyUp(MidiChannel.All, (int)keyboard.KeymappingKeyToNote[keyOnKeyboard]))
        {
            audioSource.time = audioSource.clip.length * 0.8f;
        }
        HandleAlternativeComputerKeyboardInput();
    }


    private void HandleAlternativeComputerKeyboardInput()
    {
        float strenght = Input.GetKey((KeyCode)keyboard.KeymappingNoteToComputerKeyboard[note])?1f:0f;

        if (Input.GetKeyDown((KeyCode)keyboard.KeymappingNoteToComputerKeyboard[note]))
        {
            audioSource.volume = strenght;
            audioSource.pitch = Mathf.Pow(2, ((int)keyOnKeyboard + Keyboard.transpose) / 12f); // pitch shift for the key
            audioSource.time = 0;
            justPressed = true;
            audioSource.Play();
        }

        if (audioSource.isPlaying && Input.GetKey((KeyCode)keyboard.KeymappingNoteToComputerKeyboard[note]))
        {
            if (audioSource.isPlaying)
            {
                meshRenderer.material = redMat;

            }
        }
        else
        {
            meshRenderer.material = defaultMat;
            justPressed = false;

        }
        if (audioSource.isPlaying && Input.GetKeyUp((KeyCode)keyboard.KeymappingNoteToComputerKeyboard[note]))
        {
            audioSource.time = audioSource.clip.length * 0.8f;
        }
    }



}
