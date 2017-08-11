using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AboutController : MonoBehaviour {

	public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
