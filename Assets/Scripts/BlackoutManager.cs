using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    private int awake = 0;
    private int finalAwake = 3;
    public GameObject wall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Statue")){

            StatueInfo info = collision.gameObject.GetComponent<StatueInfo>();

            if (info != null && !info.alreadyTouched)
            {
                info.alreadyTouched = true;
                AddAwake();
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    void AddAwake()
    {
        awake++;
        Debug.Log("Você conseguiu despertar o começo da sua maldição");
        if (awake >= finalAwake)
        {
            UnlockPath();
        }

    }

    void UnlockPath()
    {
       
        if (wall != null)
        {
             Debug.Log("O caminho foi liberado!");
            wall.SetActive(false);
        }
    }
}