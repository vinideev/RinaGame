using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public string gameSceneName;
    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);

    }

    public void Quit()
    {
        Application.Quit();
    }

}

