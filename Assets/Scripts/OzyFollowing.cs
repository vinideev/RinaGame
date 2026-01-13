using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class OzyFollowing : MonoBehaviour
{
    public Transform rinaPosition;
    public Rigidbody2D rigOzy;
    public float moveSpeed;
    public float stopingDistance;
    public bool isContolled;
    private Vector2 moveInput;
    public Vector3 offset = new Vector3(1.5f, 0, 0);

    void Start()
    {
     
        rigOzy = GetComponent<Rigidbody2D>();
     
    }

    void Update()
    {
        if (isContolled)

        {
        rigOzy.linearVelocity = moveInput * moveSpeed;

        }

        else
        {
        transform.position = rinaPosition.position + offset;
        rigOzy.linearVelocity = Vector2.zero;

        }
  }
    public void Move(InputAction.CallbackContext context)

    {
    if (isContolled)
    {
     
            moveInput = context.ReadValue<Vector2>();
     }

    else
    {
     
            moveInput = Vector2.zero;
    }
    }
    public void Change()

    {
    isContolled = !isContolled;
    moveInput = Vector2.zero;
    rigOzy.linearVelocity *= Vector2.zero;

    if (rinaPosition != null)
    {
         Movement rina = rinaPosition.GetComponent<Movement>();

            if (rina != null)
            {
            rina.isControlling = !isContolled;
            }
        }

    }

}
