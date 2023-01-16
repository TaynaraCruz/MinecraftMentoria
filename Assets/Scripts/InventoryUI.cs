using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] private InventoryCell[] items; 
    
    private void Start()
    {
        if (items.Length < inventory.GetInventorySize())
        {
            Debug.LogError("Inventory Size Mismatch");
            throw new NotSupportedException();
            
        }
        EventsManager.OnInventoryChange += UpdateInventoryUI;
        EventsManager.OnMouseScroll += UpdateInventorySelectedCell;
        UpdateInventoryUI();
        UpdateInventorySelectedCell(0);
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            items[i].UpdateItemCount(inventory.GetAmountOfItemInInventory(i));
        }
    }
    
    private void UpdateInventorySelectedCell(int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].UnSelectedCell();
        }
        items[index].SelectedCell();
    }

    public int GetInventoryUISize()
    {
        return items.Length;
    }
    
}
