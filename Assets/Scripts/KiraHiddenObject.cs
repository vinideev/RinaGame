using UnityEngine;

public class KiraHiddenObject : MonoBehaviour
{
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            render.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            render.enabled = false;
        }
    }
}
