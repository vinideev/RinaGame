using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour, IControllable

{
    public Rigidbody2D rigRina;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public Animator anim;
    bool isMoving;

    public AudioSource audioSource;
    public AudioClip stepSound;

    void Awake()
    {
        rigRina = GetComponent<Rigidbody2D>();
         anim = GetComponent<Animator>();

     if (audioSource == null) audioSource = GetComponent<AudioSource>();

        anim.SetFloat("Horizontal", 0f);
        anim.SetFloat("Vertical", -1f);
    }
    public void PlayFootstep()
    {
        if (isMoving && audioSource != null && stepSound != null)
        {
            audioSource.clip = stepSound;
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + 0.3f);
        }
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
