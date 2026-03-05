using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] private float gameOverDelaySeconds = 2f;
    private bool isGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void TriggerGameOver(string reason = "")
    {
        if (isGameOver) return;
        
        isGameOver = true;
        Time.timeScale = 0f; // Pausa o jogo
        
        Debug.Log($"GAME OVER: {reason}");
        
        // Aqui você pode ativar uma UI de Game Over, tocar sound, etc
        
        Invoke(nameof(RestartScene), gameOverDelaySeconds);
    }

    void RestartScene()
    {
        Time.timeScale = 1f; // Despausa o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver => isGameOver;
}
