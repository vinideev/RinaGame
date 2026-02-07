using System.Collections;
using UnityEngine;

public class KillDoor : MonoBehaviour
{
    public Transform pontoDeSpawn;
    public float tempoDeEspera = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SequenciaDeMorte(collision.gameObject));
        }
    }

    IEnumerator SequenciaDeMorte(GameObject player)
    {
        player.GetComponent<MonoBehaviour>().enabled = false;
        yield return new WaitForSeconds(tempoDeEspera);

        player.transform.position = pontoDeSpawn.position;

        player.GetComponent<MonoBehaviour>().enabled = true;

    }
}
