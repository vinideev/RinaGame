using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class FamilarInvoke : MonoBehaviour, IControllable
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

    public void InvokeCat()
    {
        // Aqui você pode colocar lógica extra se quiser
        Debug.Log("Cat recebeu comando de troca!");
    }

    void Update()
    {
        rigOzy.linearVelocity = moveInput * moveSpeed;
    }


}
