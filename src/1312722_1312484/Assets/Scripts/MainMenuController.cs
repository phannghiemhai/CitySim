using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //Simulate
    public void Simulate()
    {
        SceneManager.LoadScene("Setting");
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
