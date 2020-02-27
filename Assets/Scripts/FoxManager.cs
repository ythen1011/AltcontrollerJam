using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxManager : MonoBehaviour
{


    private static FoxManager _instance;

    public static FoxManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    [SerializeField] GameObject foxObject;
    [SerializeField] List<GameObject> foxSpawnPoint = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateFoxes(List<ChickenController> chickens)
    {
        foreach (ChickenController chick in chickens)
        {
            Debug.Assert(foxSpawnPoint.Count > 0);
            GameObject fox = Instantiate(foxObject, foxSpawnPoint[Random.Range(0, foxSpawnPoint.Count - 1)].transform.position, Quaternion.identity);
        }
    }
}
