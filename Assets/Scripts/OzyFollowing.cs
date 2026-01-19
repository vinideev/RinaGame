using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class OzyFollowing : MonoBehaviour, IControllable
{
    public Rigidbody2D rigOzy;
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    void Awake()
    {
        rigOzy = GetComponent<Rigidbody2D>();
    }

    public void OnMove(Vector2 input)
    {
        moveInput = input;
    }

    public void OnChange()
    {
        // Aqui você pode colocar lógica extra se quiser
        Debug.Log("Ozy recebeu comando de troca!");
    }

    void Update()
    {
        rigOzy.linearVelocity = moveInput * moveSpeed;
    }


}
