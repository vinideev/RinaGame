using UnityEngine;

public class RedDollInteraction : MonoBehaviour, IInteractable
{
    private bool hasInteracted = false;

    public void Interact()
    {
        if (hasInteracted) return;

        hasInteracted = true;
        
        // Marca que o player interagiu com a boneca vermelha
        DollRandomizer.MarkRedDollInteraction();
        
        // Aqui você pode adicionar outras coisas quando interage com a boneca:
        // - Diálogo
        // - Animação
        // - Som
        // - Efeito visual
        
        Debug.Log("O player interagiu com a boneca vermelha...");
    }
}
