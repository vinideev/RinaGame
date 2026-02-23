using UnityEngine;

public class DollRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject redDoll;
    [SerializeField] private GameObject blueDoll;
    [SerializeField] [Range(0f, 1f)] private float redDollChance = 0.6f;

    public bool IsRedDoll { get; private set; }

    void Start()
    {
        IsRedDoll = Random.value <= redDollChance;
        redDoll.SetActive(IsRedDoll);
        blueDoll.SetActive(!IsRedDoll);
    }
}
