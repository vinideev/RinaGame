using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    private int awake = 0;
    private int finalAwake = 3;
    public GameObject wall;

    public int AwakeCount
    {
        get { return awake; }
        set { awake = value; }
    }

    public void SetWallState(bool unlocked)
    {
        if (wall != null)
            wall.SetActive(!unlocked);
    }

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
        Debug.Log("Voc� conseguiu despertar o come�o da sua maldi��o");
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