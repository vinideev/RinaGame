using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private bool autoTrigger;
    [SerializeField] private bool oneShot;
    [SerializeField] private GameObject interactIndicator;

    private bool playerInRange;
    private bool alreadyTriggered;

    void Start()
    {
        if (interactIndicator != null)
            interactIndicator.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange || autoTrigger) return;
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsInDialogue) return;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (oneShot && alreadyTriggered) return;

        playerInRange = true;

        if (autoTrigger)
        {
            Interact();
        }
        else
        {
            if (interactIndicator != null)
                interactIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerInRange = false;

        if (interactIndicator != null)
            interactIndicator.SetActive(false);
    }

    public void Interact()
    {
        if (DialogueManager.Instance == null || dialogueData == null) return;
        if (DialogueManager.Instance.IsInDialogue) return;
        if (oneShot && alreadyTriggered) return;

        alreadyTriggered = true;

        if (interactIndicator != null)
            interactIndicator.SetActive(false);

        DialogueManager.Instance.StartDialogue(dialogueData);
    }
}