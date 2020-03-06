using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class randommusicgenerator : MonoBehaviour
{
    public List<GameObject> liGoSpawn = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {

        int one = Random.Range(0, liGoSpawn.Count);
        int two = Random.Range(0, liGoSpawn.Count);
        int three = one < two ? one : two;
        GameObject goToSpawn = liGoSpawn[three];
        Instantiate(goToSpawn, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    
}
