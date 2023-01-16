using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateBlocks : MonoBehaviour
{
    [SerializeField] private int maxDistance;
    [SerializeField] private GameObject thirdPersonView;
    [SerializeField] private GameObject firstPersonView;
    [SerializeField] private BlocksManager blocksManager;
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUI inventoryUI;
    private GameObject[] _blocks;
    private int _blockIndex = 0;
    private RaycastHit _hit;
    private Dictionary<string, int> _blockMap;

    private void Start()
    {
        _blocks = blocksManager.GetBlocks();
        _blockMap = new Dictionary<string, int>();
        for (int i = 0; i < _blocks.Length; i++) 
        {
            _blockMap.Add(_blocks[i].tag, i);
        }
    }

    void Update()
    {
        SelectBlock();
        
        if (Physics.Raycast(firstPersonView.transform.position, firstPersonView.transform.forward, out _hit,
                maxDistance))
        {
            Debug.DrawLine(firstPersonView.transform.position, _hit.point, Color.magenta);
            Debug.DrawLine(_hit.point, _hit.point + _hit.normal/2, Color.blue);
            AddBlock(_hit);
            DeleteBlock(_hit);
        }
    }

    private void SelectBlock()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _blockIndex = (_blockIndex + 1) % inventoryUI.GetInventoryUISize();
            EventsManager.RaiseOnMouseScroll(_blockIndex);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _blockIndex = _blockIndex - 1 < 0 ? inventoryUI.GetInventoryUISize() - 1 : _blockIndex - 1;
            EventsManager.RaiseOnMouseScroll(_blockIndex);
        }
    }
    private void AddBlock(RaycastHit hit)
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (inventory.IsInInventory(_blockIndex))
            {
                var position = hit.point + hit.normal/2; //Evita que um bloco seja instaciado dentro do outro
            
                position = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y),
                    Mathf.RoundToInt(position.z));

                Instantiate(_blocks[_blockIndex], position, Quaternion.identity);
                inventory.RemoveItemFromInventory(_blockIndex);
            }
            
        }
    }

    private void DeleteBlock(RaycastHit hit)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var block = hit.transform.gameObject;
            if (_blockMap.ContainsKey(block.tag))
            {
                if (inventory.PutItemInInventory(_blockMap[block.tag], 1))
                {
                    Destroy(block);
                }
            }
        }
    }

}
