using UnityEngine;

public class OzyInteract : MonoBehaviour , IInteractable
{
    
    public void Change()
    {
        ChangePlayer();

    }

    void ChangePlayer()
    {

        OzyFollowing ozy = GameObject.FindAnyObjectByType<OzyFollowing>();
        if (ozy != null) ozy.OnChange();
    }

}
