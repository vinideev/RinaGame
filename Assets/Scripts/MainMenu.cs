using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
<<<<<<< Updated upstream
    public void Play()
    {
        SceneManager.LoadScene("SceneTwo");
=======

    public string gameSceneName;
    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
>>>>>>> Stashed changes
    }

    public void Quit()
    {
        Application.Quit();
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
