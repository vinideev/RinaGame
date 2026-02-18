using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public float playerPosX;
    public float playerPosY;
    public int awakeCount;
    public List<int> touchedStatueIds = new List<int>();
    public bool wallUnlocked;
    public List<string> collectedItems = new List<string>();
    public string activeCharacter = "Rina";
}
