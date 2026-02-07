using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    private int awake = 0;
    public string[] statues;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Statue")){

            AddAwake();
            //Destroy(gameObject.CompareTag(statues[]));
        }
    }

    void AddAwake()
    {
        awake++;
        Debug.Log("Você conseguiu despertar o começo da sua maldição");
    }
}