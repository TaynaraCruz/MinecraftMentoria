using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    // [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private GameObject border;
    public void UpdateItemCount(int itemAmount)
    {
        itemCount.text = itemAmount.ToString();
    }

    public void SelectedCell()
    {
        border.SetActive(true);
    }
    
    public void UnSelectedCell()
    {
        border.SetActive(false);
    }
}