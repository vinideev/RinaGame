using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    [SerializeField] private PauseMenu pauseMenu;

    private PlayerInput playerInput;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerInput = GetComponent<PlayerInput>();

        IsPaused = false;
        Time.timeScale = 1f;
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Enable();
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Bloqueia ESC durante diálogo
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsInDialogue)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        Time.timeScale = 0f;

        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Disable();

        if (pauseMenu != null)
            pauseMenu.Show();
    }

    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null)
            pauseMenu.Hide();

        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Enable();

        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
            pc.OnMove(default);
    }
}
