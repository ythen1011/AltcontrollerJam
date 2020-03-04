using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randommusicgenerator : MonoBehaviour
{
    public List<GameObject> liGoSpawn = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
        GameObject goToSpawn = liGoSpawn[Random.Range(0, liGoSpawn.Count)];
        Instantiate(goToSpawn, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    
}
