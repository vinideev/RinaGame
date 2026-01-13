using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rig;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public bool isControlling = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
    

        if (!isControlling)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            return;
        }

        rig.linearVelocity = moveInput * moveSpeed;


    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }
}
