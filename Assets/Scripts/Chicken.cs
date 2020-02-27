using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hit detection collision check

public class Chicken : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    //public GameObject death;
    private void OnTriggerEnter(Collider Fox)
    {
        Debug.Log("hit detected");
        // GameObject d = Instantiate(death) as GameObject;
        //d.transform.position = transform.position;
        // Destroy(Fox.gameObject);
        // this.gameObject.SetActive(false);

        player.transform.position = respawnPoint.transform.position;      
    }
    



}
