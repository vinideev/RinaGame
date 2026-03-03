using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KiraDoor : MonoBehaviour
{
    [Header("Cena")]
    public string nextScene;
    public string spawnPointId;

    [Header("UI de Confirmação")]
    public GameObject confirmPanel;

    private bool isShowingPrompt;

    void Start()
    {
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (isShowingPrompt) return;

        ShowPrompt();
    }

    private void ShowPrompt()
    {
        isShowingPrompt = true;

        if (confirmPanel != null)
            confirmPanel.SetActive(true);

        PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Disable();
    }

    private void HidePrompt()
    {
        isShowingPrompt = false;

        if (confirmPanel != null)
            confirmPanel.SetActive(false);

        PlayerInput playerInput = FindAnyObjectByType<PlayerInput>();
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Enable();

        PlayerController pc = FindAnyObjectByType<PlayerController>();
        if (pc != null)
            pc.OnMove(default);
    }

    public void OnSimClicked()
    {
        NextScene.pendingStartAsKira = true;
        NextScene.SetPendingSpawnPoint(spawnPointId);
        SceneManager.LoadScene(nextScene);
    }

    public void OnNaoClicked()
    {
        HidePrompt();
    }
}
