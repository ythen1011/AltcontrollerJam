using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public void PlayGame ()
    {
        
        SceneManager.LoadScene("PullingItAllTogether");

    }
    public void HighScore()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void QuitGame ()
    {
        Debug.Log("hello");
        Application.Quit();
    }
}
