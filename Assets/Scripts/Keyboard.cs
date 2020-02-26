using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Keyboard : MonoBehaviour
{

   // InputDevice piano;
   
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(InputSystem.devices.ToString());
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
