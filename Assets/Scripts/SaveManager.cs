using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");
    private static SaveData pendingLoad;

    public static bool HasSaveFile()
    {
        return File.Exists(SavePath);
    }

    public static void Save()
    {
        SaveData data = GatherSaveData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Jogo salvo em: " + SavePath);
    }

    public static void Load()
    {
        if (!HasSaveFile())
        {
            Debug.LogWarning("Nenhum save encontrado.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        pendingLoad = JsonUtility.FromJson<SaveData>(json);

        SceneManager.sceneLoaded += OnSceneLoaded;
        Time.timeScale = 1f;
        SceneManager.LoadScene(pendingLoad.sceneName);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (pendingLoad == null) return;

        ApplySaveData(pendingLoad);
        pendingLoad = null;
    }

    private static SaveData GatherSaveData()
    {
        SaveData data = new SaveData();
        data.sceneName = SceneManager.GetActiveScene().name;

        Movement rina = Object.FindAnyObjectByType<Movement>();
        if (rina != null)
        {
            data.playerPosX = rina.transform.position.x;
            data.playerPosY = rina.transform.position.y;
        }

        BlackoutManager blackout = Object.FindAnyObjectByType<BlackoutManager>();
        if (blackout != null)
        {
            data.awakeCount = blackout.AwakeCount;
            data.wallUnlocked = blackout.AwakeCount >= 3;
        }

        StatueInfo[] statues = Object.FindObjectsByType<StatueInfo>(FindObjectsSortMode.None);
        data.touchedStatueIds = new System.Collections.Generic.List<int>();
        foreach (StatueInfo statue in statues)
        {
            if (statue.alreadyTouched)
                data.touchedStatueIds.Add(statue.statueId);
        }

        if (InventoryManager.Instance != null)
        {
            data.collectedItems = new System.Collections.Generic.List<string>(InventoryManager.Instance.GetItems());
        }

        PlayerController pc = Object.FindAnyObjectByType<PlayerController>();
        if (pc != null)
        {
            data.activeCharacter = "Rina";
        }

        return data;
    }

    private static void ApplySaveData(SaveData data)
    {
        Movement rina = Object.FindAnyObjectByType<Movement>();
        if (rina != null)
        {
            rina.transform.position = new Vector3(data.playerPosX, data.playerPosY, 0f);
        }

        BlackoutManager blackout = Object.FindAnyObjectByType<BlackoutManager>();
        if (blackout != null)
        {
            blackout.AwakeCount = data.awakeCount;
            blackout.SetWallState(data.wallUnlocked);
        }

        StatueInfo[] statues = Object.FindObjectsByType<StatueInfo>(FindObjectsSortMode.None);
        foreach (StatueInfo statue in statues)
        {
            if (data.touchedStatueIds.Contains(statue.statueId))
            {
                statue.alreadyTouched = true;
                SpriteRenderer sr = statue.GetComponent<SpriteRenderer>();
                if (sr != null) sr.color = Color.green;
            }
        }

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.SetItems(data.collectedItems);
        }

        Debug.Log("Save carregado com sucesso.");
    }
}
