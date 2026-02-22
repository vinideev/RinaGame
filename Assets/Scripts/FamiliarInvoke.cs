using UnityEngine;

public class FamilarInvoke : MonoBehaviour, IControllable
{
    public Rigidbody2D rigOzy;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public Animator anim;
    bool isMoving;

    void Awake()
    {
        rigOzy = GetComponent<Rigidbody2D>();
        
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
        // Aqui voc� pode colocar l�gica extra se quiser
        Debug.Log("Ozy recebeu comando de troca!");
    }

    public void InvokeCat()
    {
        // Aqui voc� pode colocar l�gica extra se quiser
        Debug.Log("Cat recebeu comando de troca!");
    }

    void Update()
    {
        rigOzy.linearVelocity = moveInput * moveSpeed;
    }


}
