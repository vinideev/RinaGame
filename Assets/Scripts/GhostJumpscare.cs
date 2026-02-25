using UnityEngine;

public class GhostJumpscare : MonoBehaviour
{
    [SerializeField] private DollRandomizer dollRandomizer;
    [SerializeField] private float triggerDistance = 3f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float runDistance = 10f;

    private SpriteRenderer spriteRenderer;
    private Transform player;
    private bool hasTriggered;
    private bool isRunning;
    private Vector3 runDirection;
    private float distanceRun;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        if (!hasTriggered && !isRunning)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= triggerDistance && dollRandomizer.IsRedDoll)
            {
                hasTriggered = true;
                isRunning = true;
                distanceRun = 0f;

                runDirection = (transform.position.x > player.position.x)
                    ? Vector3.left
                    : Vector3.right;

                spriteRenderer.enabled = true;
                spriteRenderer.flipX = runDirection == Vector3.left;
            }
        }

        if (isRunning)
        {
            float step = runSpeed * Time.deltaTime;
            transform.Translate(runDirection * step);
            distanceRun += step;

            if (distanceRun >= runDistance)
            {
                isRunning = false;
                gameObject.SetActive(false);
            }
        }
    }
}
