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

    public AudioSource audioSource;
    public AudioClip stepSound;


    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        string spawnId = NextScene.ConsumePendingSpawnPoint();
        if (!string.IsNullOrEmpty(spawnId))
        {
            SpawnPoint[] spawns = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
            foreach (SpawnPoint sp in spawns)
            {
                if (sp.spawnId == spawnId)
                {
                    rina.transform.position = sp.transform.position;
                    if (rinaPosition != null) rinaPosition.position = sp.transform.position;
                    break;
                }
            }
        }

        // Verifica se deve começar como Kira
        if (NextScene.ConsumePendingStartAsKira() && catPrefab != null)
        {
            // Começa apenas com Kira
            catInstance = Instantiate(catPrefab, rinaPosition.position, Quaternion.identity);
            FamilarInvoke catComponent = catInstance.GetComponent<FamilarInvoke>();
            SetCharacter(catComponent);
            
            // Desabilita Rina visualmente
            rina.gameObject.SetActive(false);
            
            // Ajusta câmeras
            cameraRina.Priority = 10;
            cameraCat.Priority = 50;
            cameraCat.Follow = catInstance.transform;
            
            Debug.Log("Sessão iniciada apenas com KIRA!");
        }
        else
        {
            // Começa normalmente com Rina
            SetCharacter(rina);
            cameraRina.Priority = 50;
            cameraCat.Priority = 10;
            Debug.Log("Sessão iniciada com Rina");
        }
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

                audioSource.PlayOneShot(stepSound);
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
                audioSource.PlayOneShot(stepSound);
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
