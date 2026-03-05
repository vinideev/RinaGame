using UnityEngine;
using UnityEngine.SceneManagement;

public class BasementGate : MonoBehaviour
{
    [SerializeField] private bool alreadyAsked = false;
    [SerializeField] private string sceneToLoad = "porão"; // nome da cena que será carregada

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (alreadyAsked) return;

        alreadyAsked = true;

        // Pergunta ao jogador se quer invocar o Kira
        if (DialogueManager.Instance == null)
        {
            // tenta criar automaticamente
            GameObject mgrObj = new GameObject("DialogueManager");
            mgrObj.AddComponent<DialogueManager>();
            Debug.LogWarning("BasementGate: DialogueManager faltando. Um novo foi criado automaticamente.");
        }

        DialogueManager.Instance.StartDialogueWithChoice(
            speaker: "Rina",
            message: "Parece que eu não consigo passar nessa porta... Que invocar o Kira?",
            onYes: OnInvokeKira,
            onNo: OnDeclineKira
        );
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reseta se sair do trigger
            alreadyAsked = false;
        }
    }

    private void OnInvokeKira()
    {
        Debug.Log("Jogador escolheu invocar o Kira!");

        // Marca que Kira foi invocado
        NextScene.SetPendingStartAsKira(true);

        // Carrega a próxima scene apenas com o Kira
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("BasementGate: sceneToLoad não definido. Defina o nome da cena no inspector.");
        }
    }

    private void OnDeclineKira()
    {
        Debug.Log("Jogador recusou invocar o Kira.");
        alreadyAsked = false; // Permite perguntar novamente
    }
}
