using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IControllable currentCharacter;

    [SerializeField] private OzyFollowing ozy;
    [SerializeField] private Movement rina;

    public GameObject ozyPrefab;
    public Transform rinaPosition;

    void Start()
    {
        SetCharacter(rina);
       
    }

    public void SetCharacter(IControllable character)
    {
        currentCharacter = character;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (currentCharacter != null)
            currentCharacter.OnMove(context.ReadValue<Vector2>());
    }

    public void OnChange(InputAction.CallbackContext context)

    {
        if (context.performed && currentCharacter != null)
        {
            currentCharacter.OnChange();

            
            if (currentCharacter == rina)
            {
                Instantiate(ozyPrefab, rinaPosition.position + new Vector3(1.5f,0,0), Quaternion.identity);
                SetCharacter(ozy);

            }
                
            else
                
                SetCharacter(rina);
                DestroyImmediate(ozy, true);

        }
    }

}
