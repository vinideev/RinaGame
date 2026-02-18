using UnityEngine;

public class HiddenItens : MonoBehaviour
{
    public GameObject cat;
    public Transform catPosition;
    private SpriteRenderer render;
    private GameObject item;
    private void Start()
    {

        //Usar apenas gameObject(com "g" min�sculo) quando quiser se referir ao objeto onde o script est�.
        item = this.gameObject;
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cat"))
        {
            Debug.Log("Opa");
            render.enabled = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        render.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }        
        GetComponent<Collider2D>().enabled = false;
        transform.position = catPosition.position;
        transform.SetParent(catPosition);
    }


}
