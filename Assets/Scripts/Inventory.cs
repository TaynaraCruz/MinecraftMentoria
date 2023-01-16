using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private int[] _itemsList;
    [SerializeField] private int maxItemAmount = 64;
    [SerializeField] private GameObject inventoryUI;
    void Start()
    {
        //load the inventory
        if (PlayerPrefs.HasKey("Inventory"))
        {
            _itemsList = PlayerPrefsX.GetIntArray("Inventory");
        }
        else //create a new inventory
        {
            PlayerPrefsX.SetIntArray("Inventory", _itemsList);
            PlayerPrefs.Save();
        }
    }
    public bool PutItemInInventory(int itemIndex,int itemAmount)
    {
        if (itemIndex < 0 || itemIndex >= _itemsList.Length)
        {
            Debug.LogError("Invalid index for inventory access");
            return false;
        }
        if (_itemsList[itemIndex] + itemAmount <= maxItemAmount)
        {
            _itemsList[itemIndex] += itemAmount;
            PlayerPrefsX.SetIntArray("Inventory", _itemsList);
            PlayerPrefs.Save();
            
            EventsManager.RaiseOnInventoryChange();
            return true;
        }
        return false;
    }
    public int GetAmountOfItemInInventory(int itemindex)
    {
        _itemsList = PlayerPrefsX.GetIntArray("Inventory");

        return _itemsList[itemindex];
    }
    public int GetInventorySize()
    {
        _itemsList = PlayerPrefsX.GetIntArray("Inventory");

        return _itemsList.Length;
    }
    public int RemoveItemFromInventory(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= _itemsList.Length)
        {
            Debug.LogError("Invalid index for inventory access");
            return -1;
        }
        if (PlayerPrefs.HasKey("Inventory"))
        {
            _itemsList = PlayerPrefsX.GetIntArray("Inventory");
        }
        
        if (_itemsList[itemIndex] > 0)
        {
            _itemsList[itemIndex]--;
        }
        
        PlayerPrefsX.SetIntArray("Inventory", _itemsList);
        PlayerPrefs.Save();
        EventsManager.RaiseOnInventoryChange();
        return _itemsList[itemIndex];
    }
    public bool IsInInventory(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= _itemsList.Length)
        {
            return false;
        }
        _itemsList = PlayerPrefsX.GetIntArray("Inventory");
        if (_itemsList[itemIndex] > 0)
        {
            return true;
        }
        return false;
    }
}

