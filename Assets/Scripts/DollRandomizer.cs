using UnityEngine;

public class DollRandomizer : MonoBehaviour
{
    public static bool IsRedDoll { get; private set; }
    public static bool PlayerInteractedWithRedDoll { get; private set; }
    public static DollRandomizer Instance { get; private set; }

    [SerializeField] private GameObject redDoll;
    [SerializeField] private GameObject blueDoll;
    [SerializeField] [Range(0f, 1f)] private float redDollChance = 0.6f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        IsRedDoll = Random.value <= redDollChance;
        PlayerInteractedWithRedDoll = false;
        redDoll.SetActive(IsRedDoll);
        blueDoll.SetActive(!IsRedDoll);
        
        Debug.Log($"Doll Selecionada: {(IsRedDoll ? "VERMELHA (PERIGOSA!)" : "AZUL (Segura)")}");
    }

    /// <summary>
    /// Marca que o jogador interagiu com a boneca vermelha
    /// </summary>
    public static void MarkRedDollInteraction()
    {
        if (IsRedDoll)
        {
            PlayerInteractedWithRedDoll = true;
            Debug.Log("⚠️ VOCÊ INTERAGIU COM A BONECA VERMELHA! Se tentar sair... GAME OVER!");
        }
    }
}
