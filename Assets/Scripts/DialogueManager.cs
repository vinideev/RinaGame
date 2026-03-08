using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public bool IsInDialogue { get; private set; }

    [SerializeField] private float dialogueDisplayDuration = 3f;

    private UnityAction onYesCallback;
    private UnityAction onNoCallback;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    /// <summary>
    /// Inicia um diálogo simples (sem escolhas)
    /// </summary>
    public void StartDialogue(DialogueData dialogueData)
    {
        if (dialogueData == null) return;
        StartCoroutine(DisplayDialogue(dialogueData));
    }

    /// <summary>
    /// Inicia um diálogo com escolha Sim/Não
    /// </summary>
    public void StartDialogueWithChoice(string speaker, string message, UnityAction onYes, UnityAction onNo)
    {
        onYesCallback = onYes;
        onNoCallback = onNo;
        
        StartCoroutine(DisplayDialogueWithChoice(speaker, message));
    }

    private IEnumerator DisplayDialogue(DialogueData dialogueData)
    {
        IsInDialogue = true;

        // Exibe cada linha do diálogo
        foreach (string line in dialogueData.lines)
        {
            Debug.Log($"{dialogueData.speakerName}: {line}");
            yield return new WaitForSeconds(dialogueDisplayDuration);
        }

        IsInDialogue = false;
    }

    private IEnumerator DisplayDialogueWithChoice(string speaker, string message)
    {
        IsInDialogue = true;

        Debug.Log($"{speaker}: {message}");
        Debug.Log("[Aguardando resposta do jogador...]");
        
        // Aqui você pode mostrar o ChoicePanel da UI
        // Por enquanto, apenas aguarda na coroutine
        
        yield return null; // Aguarda a escolha ser feita
    }

    /// <summary>
    /// Chamado quando o jogador clica em Sim
    /// </summary>
    public void AnswerYes()
    {
        if (!IsInDialogue) return;
        
        IsInDialogue = false;
        onYesCallback?.Invoke();
        onYesCallback = null;
        onNoCallback = null;
    }

    /// <summary>
    /// Chamado quando o jogador clica em Não
    /// </summary>
    public void AnswerNo()
    {
        if (!IsInDialogue) return;
        
        IsInDialogue = false;
        onNoCallback?.Invoke();
        onYesCallback = null;
        onNoCallback = null;
    }
}
