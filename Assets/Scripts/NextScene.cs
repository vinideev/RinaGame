using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextScene;
    public string spawnPointId;

    private static string pendingSpawnPointId;
    public static bool pendingStartAsKira;

    public Animator transitionAnimator;

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

    public static void SetPendingStartAsKira(bool value)
    {
        pendingStartAsKira = value;
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
           
           StartCoroutine(LoadLevel());
        }
        else
        {
            Debug.Log("Nao vai rolar");
        }
    }

    IEnumerator LoadLevel()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        pendingSpawnPointId = spawnPointId;
        SceneManager.LoadScene(nextScene);
        transitionAnimator.SetTrigger("End");


    }
}
