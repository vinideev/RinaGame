using UnityEngine;

public class DollRoomExit : MonoBehaviour
{
    [SerializeField] private bool isExitDoor = true;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isExitDoor || isTriggered) return;
        if (!collision.CompareTag("Player")) return;

        isTriggered = true;

        // Game Over apenas se for Red Doll E o player interagiu com ela
        if (DollRandomizer.IsRedDoll && DollRandomizer.PlayerInteractedWithRedDoll)
        {
            Debug.Log("Você interagiu com a boneca vermelha e agora não consegue sair!");
            GameOverManager.Instance.TriggerGameOver("A boneca vermelha matou você!");
        }
        else
        {
            Debug.Log("Você consegue sair da sala...");
            // Aqui você pode chamar NextScene.cs ou outra lógica de transição
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
