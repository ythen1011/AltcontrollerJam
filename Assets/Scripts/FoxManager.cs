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

    public List<GameObject> foxList = new List<GameObject>();

    ChordSender chordSender;

    // Start is called before the first frame update
    void Start()
    {
        chordSender = GetComponentInChildren<ChordSender>();
    }

    // Update is called once per frame
    void Update()
    {
        foxList.RemoveAll(fox => fox == null);
        foreach (GameObject fox in foxList)
        {
            if (fox.GetComponent<FoxController>().state == FoxController.foxState.toDelete)
            {
                Destroy(fox);
            }
        }
        foxList.RemoveAll(fox => fox == null);
    }

    public void SetFoxSpeed(float speed)
    {
        foreach(GameObject fox in foxList)
        {
            fox.GetComponent<FoxController>().speed = speed;
        }
    }

    public void GenerateFoxes(List<ChickenController> chickens)
    {
        foreach (ChickenController chick in chickens)
        {
            Debug.Assert(foxSpawnPoint.Count > 0);
            int index = Random.Range(0, foxSpawnPoint.Count);

            Vector3 spawnPosition = foxSpawnPoint[index].transform.position;
            //spawnPosition += 

            GameObject foxObject = Instantiate(this.foxObject, spawnPosition, Quaternion.identity);
            foxList.Add(foxObject);
            FoxController fox = foxObject.GetComponent<FoxController>();
            fox.targetChicken = chick.gameObject;
            fox.state = FoxController.foxState.chaseChicken;
        }
    }

    public bool GotAnyFoxes()
    {
        return foxList.Count > 0;
    }

}
