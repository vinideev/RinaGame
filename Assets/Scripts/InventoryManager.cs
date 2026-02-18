using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<string> items = new List<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
            Debug.Log("Item adicionado: " + itemName);
        }
    }

    public void RemoveItem(string itemName)
    {
        items.Remove(itemName);
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public List<string> GetItems()
    {
        return new List<string>(items);
    }

    public void SetItems(List<string> newItems)
    {
        items = new List<string>(newItems);
    }
}
