using UnityEngine;
using UnityEngine.UI;

public class BarrierInteract : MonoBehaviour
{
    [SerializeField] private GameObject minhaUI;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            minhaUI.SetActive(true);
        }

        if (collision.collider.CompareTag("Cat"))
        {
            minhaUI.SetActive(true);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        minhaUI.SetActive(false);
    }

   


}
