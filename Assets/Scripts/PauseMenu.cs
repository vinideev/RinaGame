using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button inventoryBackButton;

    private InventoryUI inventoryUI;

    void Awake()
    {
        inventoryUI = GetComponentInChildren<InventoryUI>(true);

        if (saveButton != null) saveButton.onClick.AddListener(OnSave);
        if (loadButton != null) loadButton.onClick.AddListener(OnLoad);
        if (inventoryButton != null) inventoryButton.onClick.AddListener(OnInventory);
        if (quitButton != null) quitButton.onClick.AddListener(OnQuit);
        if (inventoryBackButton != null) inventoryBackButton.onClick.AddListener(OnInventoryBack);

        if (menuPanel != null) menuPanel.SetActive(false);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
    }

    public void Show()
    {
        if (menuPanel != null) menuPanel.SetActive(true);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);

        if (loadButton != null) loadButton.interactable = SaveManager.HasSaveFile();
    }

    public void Hide()
    {
        if (menuPanel != null) menuPanel.SetActive(false);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
    }

    private void OnSave()
    {
        SaveManager.Save();
        if (loadButton != null) loadButton.interactable = true;
    }

    private void OnLoad()
    {
        // Restaurar estado antes de carregar cena
        if (GameManager.Instance != null)
            GameManager.Instance.Resume();
        SaveManager.Load();
    }

    private void OnInventory()
    {
        if (menuPanel != null) menuPanel.SetActive(false);
        if (inventoryPanel != null) inventoryPanel.SetActive(true);
        if (inventoryUI != null) inventoryUI.Refresh();
    }

    private void OnInventoryBack()
    {
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (menuPanel != null) menuPanel.SetActive(true);
    }

    private void OnQuit()
    {
        // Restaurar estado antes de voltar ao menu
        if (GameManager.Instance != null)
            GameManager.Instance.Resume();
        SceneManager.LoadScene("SceneOne");
    }
}
