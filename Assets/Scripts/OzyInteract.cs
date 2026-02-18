using UnityEngine;

public class OzyInteract : MonoBehaviour , IInteractable
{
    
    public void Interact()
    {
        ChangePlayer();
    }

    void ChangePlayer()
    {

        FamilarInvoke ozy = GameObject.FindAnyObjectByType<FamilarInvoke>();
        if (ozy != null) ozy.OnChange();
        FamilarInvoke cat = GameObject.FindAnyObjectByType<FamilarInvoke>();
        if (cat != null) cat.InvokeCat();
    }

}
