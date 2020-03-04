using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scoremenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("PullingItAllTogether");
    }
    
    public void QuitGame()
    {
        Debug.Log("hello");
        Application.Quit();
    }
}
