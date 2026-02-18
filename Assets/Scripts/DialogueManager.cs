using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public bool IsInDialogue { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject continueIndicator;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.03f;

    private DialogueData currentData;
    private int currentLineIndex;
    private bool isTyping;
    private Coroutine typingCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (!IsInDialogue) return;

        bool inputPressed = (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
                         || (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);

        if (inputPressed)
        {
            if (isTyping)
                SkipTyping();
            else
                DisplayNextLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        if (IsInDialogue || data == null || data.lines.Length == 0) return;

        currentData = data;
        currentLineIndex = 0;
        IsInDialogue = true;

        // Desabilita movimento do jogador
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Disable();

        // Mostra painel
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (speakerText != null)
            speakerText.text = data.speakerName;

        if (continueIndicator != null)
            continueIndicator.SetActive(false);

        DisplayLine(currentData.lines[0]);
    }

    private void DisplayNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= currentData.lines.Length)
        {
            EndDialogue();
            return;
        }

        DisplayLine(currentData.lines[currentLineIndex]);
    }

    private void DisplayLine(string line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (continueIndicator != null)
            continueIndicator.SetActive(false);

        typingCoroutine = StartCoroutine(TypewriterCoroutine(line));
    }

    private IEnumerator TypewriterCoroutine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;

        if (continueIndicator != null)
            continueIndicator.SetActive(true);
    }

    private void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = currentData.lines[currentLineIndex];
        isTyping = false;
        typingCoroutine = null;

        if (continueIndicator != null)
            continueIndicator.SetActive(true);
    }

    public void EndDialogue()
    {
        IsInDialogue = false;
        currentData = null;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Reabilita movimento
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Enable();

        // Limpa input residual
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
            pc.OnMove(default);
    }
}