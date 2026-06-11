using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] PlayerStatus player;

    List<InventoryItemCount> items = new List<InventoryItemCount>();


    public void AddItem(ItemData itemData)
    {
        foreach (InventoryItemCount item in items)
        {
            if (item.ItemData.ItemName == itemData.ItemName && item.ItemData.Type == itemData.Type)
            {
                item.Count++;

                Debug.Log($"{itemData.ItemName} 획득, 수량 : {item.Count}");
                return;
            }
        }

        InventoryItemCount newItem = new InventoryItemCount();
        newItem.ItemData = itemData;
        newItem.Count = 1;
        items.Add(newItem);

        Debug.Log($"{itemData.ItemName} 최초 획득!");
    }

    public void RemoveItem(ItemData itemData, int count = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ItemData == itemData)
            {
                items[i].Count -= count;

                Debug.Log($"{itemData.ItemName} 제거 / 남은 수량 : {items[i].Count}");

                if (items[i].Count <= 0)
                {
                    items.RemoveAt(i);
                }

                return;
            }
        }

        Debug.Log("아이템 없음");
    }

    public void UseItem(ItemType type)
    {
        for (int i = 0; i < items.Count; i++)
        {
            InventoryItemCount item = items[i];
            if (item.ItemData.Type == type)
            {
                if (type == ItemType.Potion)
                {
                    player.Heal(item.ItemData.Value);
                }
                item.Count--;
                Debug.Log($"{item.ItemData.ItemName}사용!, 수량 : {item.Count}");
                if (item.Count <= 0 )
                {
                    items.RemoveAt(i);
                }
                return;
            }


        }
        Debug.Log($"사용할 {type} 아이템이 없습니다!");


    }

}
