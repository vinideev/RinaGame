using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelController : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    void Start()
    {
        // Se não estiver atribuído no inspector, procura automaticamente
        if (yesButton == null)
        {
            Transform yesTransform = transform.Find("Yes");
            if (yesTransform != null)
                yesButton = yesTransform.GetComponent<Button>();
        }

        if (noButton == null)
        {
            Transform noTransform = transform.Find("No");
            if (noTransform != null)
                noButton = noTransform.GetComponent<Button>();
        }

        // Conecta os botões aos métodos do DialogueManager
        if (yesButton != null)
            yesButton.onClick.AddListener(() => DialogueManager.Instance.AnswerYes());

        if (noButton != null)
            noButton.onClick.AddListener(() => DialogueManager.Instance.AnswerNo());

        Debug.Log("ChoicePanel conectado aos métodos do DialogueManager!");
    }
}
