using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Debug.Log("Debug.Log(SceneManager.LoadScene(1)");
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Debug.Log("Application.Quit()");
        Application.Quit();
    }
}
