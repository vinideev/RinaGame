using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemListContainer;
    [SerializeField] private TMP_Text emptyText;

    public void Refresh()
    {
        // Limpar itens anteriores (exceto o texto de vazio)
        foreach (Transform child in itemListContainer)
        {
            if (child.gameObject != emptyText.gameObject)
                Destroy(child.gameObject);
        }

        if (InventoryManager.Instance == null) return;

        List<string> items = InventoryManager.Instance.GetItems();

        if (items.Count == 0)
        {
            if (emptyText != null) emptyText.gameObject.SetActive(true);
            return;
        }

        if (emptyText != null) emptyText.gameObject.SetActive(false);

        foreach (string item in items)
        {
            GameObject entry = new GameObject(item, typeof(RectTransform));
            entry.transform.SetParent(itemListContainer, false);

            TMP_Text text = entry.AddComponent<TextMeshProUGUI>();
            text.text = item;
            text.fontSize = 24;
            text.color = Color.white;
            text.alignment = TextAlignmentOptions.Left;
        }
    }
}
