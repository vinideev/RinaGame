using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour, IControllable

{
    public Rigidbody2D rigRina;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public Animator anim;
    bool isMoving;

    void Awake()
    {
        rigRina = GetComponent<Rigidbody2D>();
    }

    public void OnMove(Vector2 input)
    {
        moveInput = input;

        isMoving = moveInput.sqrMagnitude > 0;

        anim.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            anim.SetFloat("Horizontal", moveInput.x);
            anim.SetFloat("Vertical", moveInput.y);
        }
    }

    public void OnChange()
    {
        Debug.Log("Rina recebeu comando de troca!");
    }

    public void InvokeCat()
    {

        Debug.Log("Rina vai invocar o gato");
    }

    void Update()
    {
        rigRina.linearVelocity = moveInput * moveSpeed;
    }

}
