using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// hit detection collision check

public class Chicken : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

   // public int Score;
    public TextMeshProUGUI ScoreText;

    //public GameObject death;
    private void OnTriggerEnter(Collider Fox)
    {
        Debug.Log("hit detected");
        // GameObject d = Instantiate(death) as GameObject;
        //d.transform.position = transform.position;
        // Destroy(Fox.gameObject);
        // this.gameObject.SetActive(false);

      //  Score -= 1;
     //   ScoreText.text = "Score: " + Score.ToString();


        player.transform.position = respawnPoint.transform.position;
        Fox.transform.position = respawnPoint.transform.position;
    }
    



}
