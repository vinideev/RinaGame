using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IControllable currentCharacter;
    public CinemachineCamera cameraRina;
    public CinemachineCamera cameraOzy;
    public CinemachineCamera cameraCat;



    [SerializeField] private Movement rina;
    private GameObject ozyInstance;
    private GameObject catInstance;

    public GameObject catPrefab;
    public GameObject ozyPrefab;
    public Transform rinaPosition;


    void Start()
    {

        SetCharacter(rina);
        cameraRina.Priority = 50;
       
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
                

               cameraOzy.Priority = 40;
               cameraRina.Priority = 20;
               cameraOzy.Follow = ozyInstance.transform;
                
                
               
              
                
            }
            
            else
            {
                Destroy(ozyInstance);

                ozyInstance = null;   
                SetCharacter(rina);
                cameraOzy.Priority = 10;
                cameraRina.Priority = 30;
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
                cameraCat.Priority = 40;
                cameraRina.Priority = 20;
                cameraCat.Follow = catInstance.transform;


            }

            else
            {
                Destroy(catInstance);

                catInstance = null;
                SetCharacter(rina);
                cameraCat.Priority = 10;
                cameraRina.Priority = 30;
            }
        }
    }

}
