using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IControllable currentCharacter;


    [SerializeField] private Movement rina;
    private GameObject ozyInstance;
    private GameObject catInstance;

    public GameObject catPrefab;
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
            if (ozyInstance == null)
            {
                ozyInstance = Instantiate(ozyPrefab, rinaPosition.position + new Vector3(2f, 0, 0), Quaternion.identity);

                FamilarInvoke ozyComponent = ozyInstance.GetComponent<FamilarInvoke>();

                currentCharacter.OnChange();

                
                SetCharacter(ozyComponent);
            }
            
            else
            {
                Destroy(ozyInstance); 
                
                ozyInstance = null;   
                SetCharacter(rina);
            }
        }
    }



    public void InvokeCat(InputAction.CallbackContext context)

    {
        if (context.performed && currentCharacter != null)
        {
            if (catInstance == null)
            {
                catInstance = Instantiate(catPrefab, rinaPosition.position + new Vector3(2f, 0, 0), Quaternion.identity);

                FamilarInvoke catComponent = catInstance.GetComponent<FamilarInvoke>();

                currentCharacter.InvokeCat();


                SetCharacter(catComponent);
            }

            else
            {
                Destroy(catInstance);

                catInstance = null;
                SetCharacter(rina);
            }
        }
    }

}
