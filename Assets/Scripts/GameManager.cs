using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static object Lock = new object();
    private static GameManager instance;
    public static GameManager Instance {
        get { 

            lock (Lock)
            {
                if (Instance == null)
                {
                    // Search for existing instance.
                    instance = (GameManager) FindObjectOfType(typeof(GameManager));
 
                    // Create new instance if one doesn't already exist.
                    if (instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
 
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
 
                }
                return instance;
                
            }
        }
    }

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
