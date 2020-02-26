using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
       if( MidiMaster.GetKey(MidiChannel.All, noteNumber: 60)!=0)
        {
            Debug.Log("hi");
        }

        
        
            Debug.Log(MidiMaster.GetKey(MidiChannel.All, noteNumber: 59));
        
    }
}
