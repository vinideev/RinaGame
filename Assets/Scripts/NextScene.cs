using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextScene;
    public string spawnPointId;

    private static string pendingSpawnPointId;
    public static bool pendingStartAsKira;

    public static void SetPendingSpawnPoint(string id)
    {
        pendingSpawnPointId = id;
    }

    public static string ConsumePendingSpawnPoint()
    {
        string id = pendingSpawnPointId;
        pendingSpawnPointId = null;
        return id;
    }

    public static bool ConsumePendingStartAsKira()
    {
        bool value = pendingStartAsKira;
        pendingStartAsKira = false;
        return value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pendingSpawnPointId = spawnPointId;
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("Nao vai rolar");
        }
    }
}
